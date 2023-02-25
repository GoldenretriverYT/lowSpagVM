using libLowSpagVM;
using LowSpagVM.Common;
using System.ComponentModel;
using System.Diagnostics;

namespace DebuggingVM {
    public partial class MainVM : Form {
        public CPU CPU { get; set; }
        public BindingList<(string it, string data)> DisassembledInstructions { get; set; } = new();
        public uint DisassembledStart = 0;
        public (uint, uint) DisassembledConstantsRange = (0, 0); // Automatically inferred from first instruction

        private bool done = false, singleStep = false, isBreakpoint = false;

        public MainVM() {
            InitializeComponent();
            
            Instructions.Write = (string txt) => {
                outputTextBox.Text += txt;
            };

            disassembledInstructionsListBox.SetDoubleBuffered(true);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e) {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            openSpagBinDialog.ShowDialog();
        }

        private void openSpagBinDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
            InitCPU();
        }

        private void InitCPU() {
            done = false;
            outputTextBox.Text = "";

            try {
                CPU = CPU.Load(File.ReadAllBytes(openSpagBinDialog.FileName));

                CPU.OnBreakpoint += () => {
                    isBreakpoint = true;
                    Disassemble();
                };
                
                CPU.AfterCycle += () => {
                    isBreakpoint = false;
                    dbgStateLabel.Text = "Debugger State: OK";

                    if (onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.CheckState == CheckState.Unchecked || singleStep) {
                        SuspendLayout();
                        
                        string dumpStr = $@"Dump:
  Registers:
    ";
                        for (int i = 0; i < CPU.Registers.Length; i++) {
                            dumpStr += ("R" + i + ": " + CPU.Registers[i] + " ").PadRight(16);
                            if (i % 4 == 3) dumpStr += "\r\n    ";
                        }

                        dumpStr += $@"
  Other:
    MPTR: {CPU.MemoryPtr,-4} (0x{CPU.MemoryPtr:x4})
    PC:   {CPU.ProgramCounter,-4} (0x{CPU.ProgramCounter:x4})

    Memory near MPTR: ";

                        infoDumpBox.Text = dumpStr;

                        for (int i = -8; i < 9; i++) {
                            if (CPU.MemoryPtr + i < 0 || CPU.MemoryPtr + i >= CPU.MEMORY_SIZE) continue;

                            byte curByte = CPU.Memory.Read((uint)(CPU.MemoryPtr + i));

                            if (i == 0) {
                                infoDumpBox.SelectionColor = Color.Blue;
                                infoDumpBox.AppendText($@"{curByte:x2} ");
                                infoDumpBox.SelectionColor = Color.Black;
                            } else {
                                infoDumpBox.AppendText($@"{curByte:x2} ");
                            }
                        }

                        char mptrAscii = (char)CPU.Memory.Read(CPU.MemoryPtr);
                        infoDumpBox.AppendText($@"
    Memory at MPTR: DEC: {CPU.Memory.Read(CPU.MemoryPtr),-3} | HEX: 0x{CPU.Memory.Read(CPU.MemoryPtr):x2} | ASCII: {(char.IsControl(mptrAscii) ? "N/A" : mptrAscii.ToString())}");


                        ResumeLayout();
                    }
                    
                    Application.DoEvents();

                    singleStep = false;
                };

                CPU.ExecutionDone += () => {
                    done = true;
                };

                Disassemble(25);
            } catch (Exception ex) {
                MessageBox.Show("Loading binary failed: " + ex.Message);
                CPU = null;
            }
        }

        private void Disassemble(int range = 100) {
            uint start = (uint)Math.Clamp((int)(CPU.ProgramCounter/4) - range, 0, CPU.MEMORY_SIZE);
            uint end = (uint)Math.Clamp((CPU.ProgramCounter/4) + range, start, CPU.MEMORY_SIZE);
            
            Disassemble(start, end);
        }

        private void Disassemble(uint start, uint end) {
            if (end - start < 1) {
                MessageBox.Show("There are no instructions to dissamble! DissambleStart: " + start + "; DissambleEnd: " + end);
                return;
            }

            Debug.WriteLine("Disassembled between " + start + " and " + end);

            DisassembledInstructions.Clear();
            DisassembledStart = start;

            DisassembledConstantsRange = (4, BitConverter.ToUInt16(CPU.Memory.Read(0, 4)[1..3]));

            for (uint i = start; i < end; i++) {
                byte[] inst = CPU.Memory.Read(i * 4, 4);
                DisassembledInstructions.Add((((InstructionType)inst[0]).ToString(), inst[1] + " " + inst[2] + " " + inst[3]));
            }

            ReloadDisassemblerListView();
        }

        private void ReloadDisassemblerListView() {
            SuspendLayout();

            if(CPU.ProgramCounter % 4 != 0) {
                Debug.WriteLine("Program counter is not aligned to 4 bytes; unable to highlight current instruction!");
                dbgStateLabel.Text = "Debugger State: Program counter is not aligned to 4 bytes; unable to highlight current instruction!";
            }
            
            disassembledInstructionsListBox.Items.Clear();

            int i = 0;
            foreach(var inst in DisassembledInstructions) {
                bool IsForConstants = i * 4 >= DisassembledConstantsRange.Item1 && i * 4 < DisassembledConstantsRange.Item2;

                string type = "Code";

                if (IsForConstants) type = "Data";
                disassembledInstructionsListBox.Items.Add(new ListViewItem(new string[] {type,  "0x" + (DisassembledStart + i * 4).ToString("X4"), inst.it, inst.data}));

                if (CPU.ProgramCounter == ((DisassembledStart + i) * 4)+4) {
                    disassembledInstructionsListBox.Items[i].BackColor = (isBreakpoint ? Color.Red : Color.LightGreen);
                    disassembledInstructionsListBox.EnsureVisible(i);
                } else if (IsForConstants) {
                    disassembledInstructionsListBox.Items[i].BackColor = Color.Gray;
                }
                
                i++;
            }

            ResumeLayout(true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            if (CPU == null) {
                MessageBox.Show("Please select a executable to debug first!");
                return;
            }
            
            if (done) InitCPU(); // Reset CPU
            
            try {
                CPU.Run();
            } catch (Exception ex) {
                MessageBox.Show("CPU Error: " + ex.Message);
                
                if (resetCPUOnErrorCheck.CheckState == CheckState.Checked) {
                    done = true;
                } else {
                    CPU.IncreasePC(4);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            if(CPU == null) {
                MessageBox.Show("Please select a executable to debug first!");
                return;
            }

            if (done) InitCPU(); // Reset CPU

            try {
                singleStep = true;
                CPU.Cycle();
            } catch (Exception ex) {
                MessageBox.Show("CPU Error: " + ex.Message);

                if (resetCPUOnErrorCheck.CheckState == CheckState.Checked) {
                    done = true;
                } else {
                    CPU.IncreasePC(4);
                }
            }

            CPU.AfterCycle?.Invoke();

            Disassemble(25); // The current pc might be > disassembled instructions, so always disassemble when stepping!
            ReloadDisassemblerListView();
        }

        private void resetCPUOnErrorCheck_Click(object sender, EventArgs e) {
            resetCPUOnErrorCheck.CheckState = (resetCPUOnErrorCheck.CheckState == CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked;
        }
        
        private void onlyUpdateInformationDumpOnSingleStepToolStripMenuItem_Click(object sender, EventArgs e) {
            onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.CheckState = (onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.CheckState == CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked;
        }

        private void outputTextBox_TextChanged(object sender, EventArgs e) {

        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e) {

        }

    }
}
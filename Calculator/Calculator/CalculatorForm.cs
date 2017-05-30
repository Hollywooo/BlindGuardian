using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        Evaluator theTotal = new Evaluator(0,0,0);
        int entryCount = 0;
        
        public CalculatorForm()
        {
            InitializeComponent();
            tb_Input.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)  //I got this setup from another source.
            //I definitely do not understand 'protected' or 'override' or why you have to return a true/false.
            //I used it bc it was the only thing I could find which would allow me to trigger a different response
            //between numerical inputs and command inputs.
            //This program does not filter alpha inputs from numerical inputs. I know how it could be done but it would
            //take forever and it wasn't part of the specs.
             
        {
            switch (keyData)  //Differentiates between command key (+,-,*,/,enter) and numerical inputs
                //A switch may not be the most appropriate here, bc this actually only has one case in the end.
                //Switch here was an artifact of a previous attempt that I had started and I liked it as an easy
                //router, so I kept it.
            {

                case Keys.Add:
                case Keys.Subtract:
                case Keys.Multiply:
                case Keys.Divide:
                case Keys.Enter:

                    entryCount++;  //Initial entries must be handled differently from subsequent or sequential calculations bc there is no
                        //carry-through operator.

                    if (entryCount == 1) //Initial path
                    {
                        theTotal.currentValue += Convert.ToDouble(tb_Input.Text);  //theTotal.currentValue is assigned as 0 before this step.
                        theTotal.operationType = Evaluator.OpTypeAssign(keyData);  //records operator input for next user entry; see Evaluator.OpTypeAssign
                        tb_Result.Text = Convert.ToString(theTotal.currentValue);  //puts initial input in result box
                        tb_Input.Clear();  //obvious

                        if (theTotal.operationType == 0)  //If Enter is pressed, this treats the calculation as complete
                                                            //and resets theTotal.
                        {
                            Console.WriteLine("Operation Complete");
                            theTotal.operationType = 0;  //Different data types!!
                            theTotal.currentValue = theTotal.userInput = 0;
                            entryCount = 0;  //reset counter
                        }
                        else { }
                        
                    }
                    else  //path for sequential calculations (1 + 3 - 9 * 10), NO order of operations - purely sequential
                    {
                        
                        theTotal.currentValue = Evaluator.Computation(theTotal.currentValue, Convert.ToDouble(tb_Input.Text), theTotal.operationType);  //see Evaluator.Computation.  Computation must occur first before redefining operation type.
                        theTotal.operationType = Evaluator.OpTypeAssign(keyData);  //SAA

                        tb_Result.Text = Convert.ToString(theTotal.currentValue);  //SAA
                        tb_Input.Clear();
                        //Console.WriteLine(theTotal.currentValue);
                        if (theTotal.operationType == 0)
                        {
                            Console.WriteLine("Operation Complete");
                            theTotal.operationType = 0;
                            theTotal.currentValue = theTotal.userInput = 0;
                            entryCount = 0;
                        }
                        else { }
                        return true;
                    }
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);  //not sure the purpose of this aside that it returns the bool 
                                                            //requested in the method.

        }

        private void btn_Calculate_Click(object sender, EventArgs e)  //Purely keyboard controlled form so this doesn't do anything.
        {
            
        }
    }
}

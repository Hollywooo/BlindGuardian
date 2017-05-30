using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Input;

namespace Calculator
{
    class Evaluator
    {
        //The Evaluator class stores the three essential pieces of the calculation data: what has been calculated to this point, the most recent user input, and the operation type to be performed between the two.
        public double currentValue { get; set; }
        public double userInput { get; set; }
        public int operationType { get; set; }
       
        public Evaluator(double currentValue, double userInput, int operationType)
        {
            this.currentValue = 0;
            this.userInput = 0;
            this.operationType = 0;
        }

        public static int OpTypeAssign(Keys keyInput)  //Assigns operation type to Evaluator class based on keyboard input.
                                                        //This is necessary bc of the order of inputs - value1, (operation), value2 - the operation must be held until the next
                                                        //value is entered.  In hindsight the conversion of type Keys to int is not necessary and the Evaluator class could be 
                                                        //defined as (double, double, Keys).  I still think this step would be necessary for the workflow outlined above.
        {
            int nextOp = new int();

            switch (keyInput)
            {
                case Keys.Enter:
                    nextOp = 0;
                    return nextOp;
                case Keys.Add:
                    nextOp = 1;
                    return nextOp;
                case Keys.Subtract:
                    nextOp = 2;
                    return nextOp;
                case Keys.Multiply:
                    nextOp = 3;
                    return nextOp;
                case Keys.Divide:
                    nextOp = 4;
                    return nextOp;
            }
        
            return nextOp;  //Without this line gives error "not all paths return value", bc keyInput is "soft-limited" to these ops
                            //by the switch in CalculatorForm.cs.
        }

        public static double Computation(double currentValue, double userInput, int operationType)  //Performs calculations
        {
            double newValue = new double();  //return variable

            switch (operationType)  //uses operation definitions from OpTypeAssign to perform values now that the second user input (i.e. value2) has been provided.
            {
                case 1:
                    newValue = currentValue + userInput;
                    return newValue;
                case 2:
                    newValue = currentValue - userInput;
                    return newValue;
                case 3:
                    newValue = currentValue * userInput;
                    return newValue;
                case 4:
                    newValue = currentValue / userInput;
                    return newValue;

            }
            return newValue;  //SAA
        }
    }
}

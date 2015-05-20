using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.ComponentModel;

namespace CAIN
{
    /// <summary>
    ///    Clase para el manejo del registro del proceso de catalogación.
    /// </summary>
    public class Logger
    {
        /* Tipos de escritura que la clase puede manejar */

        private enum LoggerTypes { FileWriter, ConsoleWriter, ControlWriter };

        /* Atributos para saber si */

        private LoggerTypes Type;
        private StreamWriter writer = null;
        private Control control = null;

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Logger()
        {
            this.Type = LoggerTypes.ConsoleWriter;
        }

        /// <summary>
        ///    Constructor.
        /// </summary>    
        /// <param name="file">
        ///    El nombre del archivo donde se escribirá la información del registro.
        /// </param>       
        public Logger(string file)
        {
            this.Type = LoggerTypes.FileWriter;
            
            this.writer = new StreamWriter(file);
        }

        /// <summary>
        ///    Constructor.
        /// </summary>    
        /// <param name="control">
        ///    El control de texto (p.e. ListBox) donde se escribirá la información del registro.
        /// </param>       
        public Logger(Control control)
        {
            this.Type = LoggerTypes.ControlWriter;

            this.control = control;
        }

        ~Logger()
        {   
            if (this.writer != null)
                this.writer.Close();
        }

        /// <summary>
        ///    Método que permite escribir una cadena de texto.
        /// </summary>    
        /// <param name="str">
        ///    La cadena de texto.
        /// </param>       
        public void Write(string str)
        {
            switch (this.Type)
            {
                case LoggerTypes.FileWriter:
                    this.writer.Write(str);
                    break;
                case LoggerTypes.ConsoleWriter:
                    Console.Write(str);
                    break;
                case LoggerTypes.ControlWriter:
                    if (this.control is Label)
                    {
                        Label label = this.control as Label;
                        label.Invoke(new Action(() => label.Text = str));
                    }
                    else if (this.control is ListBox)
                    {
                        ListBox listBox = this.control as ListBox;
                        listBox.Invoke(new Action(() => listBox.Items.Add(str)));
                    }
                    break;
            }

            /*if (this.WriteToFile)
            {
                this.writer.Write(str);
            }
            else if (this.WriteToConsole)
            {
                Console.Write(str);
            }
            else if (this.WriteToControl)
            {
                if (this.control is Label)
                {
                    Label label = this.control as Label;
                    label.Invoke(new Action(() => label.Text = str));
                }
                else if (this.control is ListBox)
                {
                    ListBox listBox = this.control as ListBox;
                    listBox.Invoke(new Action(() => listBox.Items.Add(str)));
                }
            }*/
        }

        /// <summary>
        ///    Método que permite escribir una línea de texto.
        /// </summary>    
        /// <param name="str">
        ///    La cadena de texto.
        /// </param>       
        public void WriteLine(string str)
        {
            switch (this.Type)
            {
                case LoggerTypes.FileWriter:
                    this.writer.WriteLine(str);
                    break;
                case LoggerTypes.ConsoleWriter:
                    Console.WriteLine(str);
                    break;
                case LoggerTypes.ControlWriter:
                    if (this.control is Label)
                    {
                        Label label = this.control as Label;
                        label.Invoke(new Action(() => label.Text = str));
                    }
                    else if (this.control is ListBox)
                    {
                        ListBox listBox = this.control as ListBox;
                        listBox.Invoke(new Action(() => listBox.Items.Add(str)));
                        listBox.Invoke(new Action(() => listBox.TopIndex = listBox.Items.Count - listBox.ClientSize.Height / listBox.ItemHeight + 1));
                    }
                    break;
            } 
            
            /*if (this.WriteToFile)
            {
                this.writer.WriteLine(str);
            }
            else if (this.WriteToConsole)
            {
                Console.WriteLine(str);
            }
            else if (this.WriteToControl)
            {
                if (this.control is Label)
                {
                    Label label = this.control as Label;
                    label.Invoke(new Action(() => label.Text = str));
                }
                else if (this.control is ListBox)
                {
                    ListBox listBox = this.control as ListBox;
                    listBox.Invoke(new Action(() => listBox.Items.Add(str)));      
                    listBox.Invoke(new Action(() => listBox.TopIndex = listBox.Items.Count - listBox.ClientSize.Height / listBox.ItemHeight + 1));
                    //listBox.Invoke(new Action(() => listBox.SelectedIndex = listBox.Items.Count-1));
                    //listBox.Invoke(new Action(() => listBox.SelectedIndex = -1));
                }
            }*/
        }
    }
}

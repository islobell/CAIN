using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAINClient
{
    /// <summary>
    ///    Clase para el filtrado de las entidades de la vista del panel derecho.
    /// </summary>
    public class EntityFilter
    {
        /// <summary>
        ///    Método que permite filtrar comprueba si se ha realizado cambios en el contenido de uno o varios controles del panel izquierdo.
        /// </summary>
        /// <param name="view">
        ///    La vista a la que pertenecen las columnas.
        /// </param> 
        /// <param name="columnIndex">
        ///    La posición de la columna en la lista de columnas de la vista.
        /// </param> 
        /// <param name="entities">
        ///    La lista de entidades a filtrar.
        /// </param> 
        /// <param name="operation">
        ///    La operación a realizar sobre el patrón de filtrado.
        /// </param> 
        /// <param name="pattern">
        ///    El patrón de filtrado.
        /// </param> 
        /// <returns>
        ///    La lista de entidades que cumplen el patrón de filtrado.
        /// </returns>       
        public static List<CAIN.Entity> Filter(DataGridView view, int columnIndex, List<CAIN.Entity> entities, string operation, string pattern)
        {
            if (columnIndex == -1 || entities.Count == 0 || String.IsNullOrEmpty(operation) || String.IsNullOrEmpty(pattern))
                return entities;

            List<CAIN.Entity> displayedEntities = new List<CAIN.Entity>();

            /* Obtenemos el metadato de la columna que nos indica la clase y la propiedad de las entidades que representa el contenido de la columna en cuestión */

            string tag = view.Columns[columnIndex].Tag.ToString();

            /* Separamos la clase y la propiedad para manipularlas mejor */

            string[] items = tag.Split(new char[]{'.'});
            Debug.Assert(items.Length == 2);

            List<string> elements = new List<string>();

            /* A partir de la clase y la propiedad, obtenemos una lista de cadenas de texto */

            switch (items[0])
            {
                case "Track":
                    if (items[1] == "Title")
                        elements = entities.Select(item => item.Track.Title).ToList();
                    else if (items[1] == "Duration")
                        elements = entities.Select(item => TimeSpan.FromSeconds(item.Track.Duration).ToString(@"mm\:ss")).ToList();
                    else if (items[1] == "Status")
                        elements = entities.Select(item => CAIN.Utils.GetStringFromStatus(item.Track.Status)).ToList();
                    else
                        Debug.Assert(false);
                    break;
                case "Album":
                    if (items[1] == "Title")
                        elements = entities.Select(item => item.Album.Title).ToList();
                    else if (items[1] == "Year")
                        elements = entities.Select(item => item.Album.Year.ToString()).ToList();
                    else
                        Debug.Assert(false);
                    break;
                case "Artist":
                    elements = entities.Select(item => String.Join(", ", item.Artists.Select(y => y.Name))).ToList();
                    break;
                case "Tag":
                    elements = entities.Select(item => item.Tags.FirstOrDefault(y => y.Name == view.Columns[columnIndex].HeaderText).Name).ToList();
                    break;
                default: 
                    Debug.Assert(false); 
                    break;
            }

            /* Obtenemos la lista de entidades a mostrar usando la lista de cadenas de texto, la operación de filtrado y el patrón a aplicar */

            return PatternMatch(elements, entities, operation, pattern);
        }

        /// <summary>
        ///    Método que permite filtrar las entidades de la vista del panel derecho.
        /// </summary>
        /// <param name="elements">
        ///    La lista de cadenas que se usará durante el filtrado.
        /// </param> 
        /// <param name="entities">
        ///    La lista de entidades a filtrar.
        /// </param> 
        /// <param name="operation">
        ///    La operación a realizar sobre el patrón de filtrado.
        /// </param> 
        /// <param name="pattern">
        ///    El patrón de filtrado.
        /// </param> 
        /// <returns>
        ///    La lista de entidades que cumplen el patrón de filtrado.
        /// </returns>
        private static List<CAIN.Entity> PatternMatch(List<string> elements, List<CAIN.Entity> entities, string operation, string pattern)
        {
            List<CAIN.Entity> displayedEntities = new List<CAIN.Entity>();

            for (int i = 0; i < elements.Count; i++)
            {
                string value = elements[i];
                string regexPattern = String.Empty;

                /* Con la operación de filtrado y el patrón, contruimos una expresión regular que se aplicará a lista de cadenas de texto */

                switch (operation)
                {
                    case "Empieza por":
                        regexPattern = "^(" + pattern + ")";
                        break;
                    case "No empieza por":
                        regexPattern = "^(?!" + pattern + ")";
                        break;
                    case "Termina por":
                        regexPattern = "(" + pattern + ")$";
                        break;
                    case "No termina por":
                        regexPattern = "(?!" + pattern + ")$";
                        break;
                    case "Contiene":
                        regexPattern = "(.*" + pattern + ".*)";
                        break;
                    case "No contiene":
                        regexPattern = "^(?!(.*" + pattern + ").*)";
                        break;
                    case "Es igual a":
                        regexPattern = "^" + pattern + "$";
                        break;
                    case "No es igual a":
                        regexPattern = "^(?!" + pattern + ").*$";
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

                Regex rgx = new Regex(regexPattern, RegexOptions.IgnoreCase);

                if (rgx.IsMatch(value)) 
                    displayedEntities.Add(entities[i]);
            }

            return displayedEntities;
        }
    }
}

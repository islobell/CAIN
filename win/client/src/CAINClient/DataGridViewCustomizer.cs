using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAINClient
{
    /// <summary>
    ///    Clase para la manipulación de controles de tipo DataGridView.
    /// </summary>
    class DataGridViewCustomizer
    {                                                                       
        private static List<SortOrder> OrderBy;    ///< Lista de ordenaciones que se aplica a cada una de las columnas

        /// <summary>
        ///    Método que permite establecer las columnas de una vista.
        /// </summary> 
        /// <param name="view">
        ///    El objeto DataGridView que se va a manipular.
        /// </param>  
        /// <param name="columns">
        ///    La lista de nombres de columna que se asignarán a la vista.
        /// </param>
        public static void SetColumns(DataGridView view, List<string> columns)
        {
            /* Borramos las columnas */

            view.Columns.Clear();

            int columCount = 0;

            DataGridViewTextBoxColumn col;

            /* Para cada nombre de la lista de columnas, creamos una columna en la vista */

            foreach (string column in columns)
            {
                col = new DataGridViewTextBoxColumn();
                switch (column)
                {
                    case "Título":
                        col.HeaderText = "Título";
                        col.FillWeight = 0.35f;
                        col.MinimumWidth = 100;
                        col.Width = 200;
                        col.Tag = "Track.Title";
                        break;
                    case "Duración":
                        col.HeaderText = "Duración";
                        col.FillWeight = 0.05f;
                        col.MinimumWidth = 60;
                        col.Width = 100;
                        col.Tag = "Track.Duration";
                        break;
                    case "Álbum":
                        col.HeaderText = "Álbum";
                        col.FillWeight = 0.35f;
                        col.MinimumWidth = 100;
                        col.Width = 200;
                        col.Tag = "Album.Title";
                        break;
                    case "Año":
                        col.HeaderText = "Año";
                        col.FillWeight = 0.05f;
                        col.MinimumWidth = 60;
                        col.Width = 100;
                        col.Tag = "Album.Year";
                        break;
                    case "Artista(s)":
                        col.HeaderText = "Artista(s)";
                        col.FillWeight = 0.15f;
                        col.MinimumWidth = 100;
                        col.Width = 150;
                        col.Tag = "Artist.Name";
                        break;
                    case "Estado":
                        col.HeaderText = "Estado";
                        col.FillWeight = 0.05f;
                        col.MinimumWidth = 90;
                        col.Width = 100;
                        col.Tag = "Track.Status";
                        break;
                    default:
                        col.HeaderText = column;
                        col.FillWeight = 0.15f;
                        col.MinimumWidth = 80;
                        col.Width = 150;
                        col.Tag = "Tag.Name";
                        break;
                }

                /* Las columnas no podrán ordenarse de forma automática */

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                view.Columns.Add(col);
                columCount++;
            }

            /* Creamos la lista de ordenaciones */

            OrderBy = Enumerable.Repeat(SortOrder.None, columCount).ToList();

            /* Establecemos el margen interior de las celdas */

            Padding newPadding = new Padding(5, 0, 5, 0);
            view.RowTemplate.DefaultCellStyle.Padding = newPadding;

            /* La cabecera de las filas no será visible */

            view.RowHeadersVisible = false;
        }

        /// <summary>
        ///    Método que permiter llenar una vista a partir de una lista de entidades.
        /// </summary> 
        /// <param name="view">
        ///    El objeto DataGridView que se va a manipular.
        /// </param>  
        /// <param name="entities">
        ///    La lista de entidades que se usará para rellenar la vista.
        /// </param>
        /// <param name="drawBKColor">
        ///    Indica si se pintará el fonde de las celdas.
        /// </param>
        public static void Fill(DataGridView view, List<CAIN.Entity> entities, bool drawBKColor = true)
        {
            /* Borramos todas las filas de la vista */

            view.Rows.Clear();

            DataGridViewColumnCollection cols = view.Columns;

            /* Para cada entidad, creamos una fila y rellenamos las celdas con la información relacionada */
            
            foreach (CAIN.Entity entity in entities)
            {
                int rowIndex = view.Rows.Add();

                DataGridViewCustomizer.UpdateRow(view, rowIndex, entity, drawBKColor);

                //DataGridViewRow row = view.Rows[rowIndex];

                //for (int i = 0; i < row.Cells.Count; i++)
                //{
                //    DataGridViewCell cell = row.Cells[i];

                //    DataGridViewCustomizer.UpdateRow(view, view.Rows.Count - 1, entity, drawBKColor);

                    ///* Pintamos el fondo de las celdas */
                    
                    //if (drawBKColor)
                    //{
                    //    if (entity.Track.Status != CAIN.Track.StatusTypes.Cataloged)
                    //        cell.Style.BackColor = Color.LightSalmon;
                    //    else
                    //    {
                    //        if (entity.Track.Reliability < 40)
                    //            cell.Style.BackColor = Color.LightSalmon;
                    //        else if (entity.Track.Reliability >= 40 && entity.Track.Reliability <= 80)
                    //            cell.Style.BackColor = Color.LightYellow;
                    //        else if (entity.Track.Reliability > 80)
                    //            cell.Style.BackColor = Color.LightGreen;
                    //    }
                    //}

                    ///* Cada celda contendrá la información que corresponda en función de la columna a la que pertenezca */

                    //switch (cols[i].HeaderText)
                    //{         
                    //    case "Título":
                    //        cell.Value = entity.Track.Title;
                    //        break;
                    //    case "Duración":
                    //        cell.Value = TimeSpan.FromSeconds(entity.Track.Duration).ToString(@"mm\:ss");
                    //        break;
                    //    case "Álbum":
                    //        cell.Value = entity.Album.Title;
                    //        break;
                    //    case "Año":
                    //        cell.Value = entity.Album.Year;
                    //        break;
                    //    case "Artista(s)":
                    //        cell.Value = String.Join(", ", entity.Artists.Select(item => item.ToString()));
                    //        break;
                    //    case "Estado":
                    //        cell.Value = CAIN.Utils.GetStringFromStatus(entity.Track.Status);
                    //        break;
                    //    default:
                    //        CAIN.Tag tag = entity.Tags.FirstOrDefault(item => item.Name == cols[i].HeaderText);
                    //        cell.Value = tag != null ? tag.Value : String.Empty;
                    //        break;
                    //}
                //}
            }
        }

        /// <summary>
        ///    Método que permite actualizar la fila de una vista con la información de una entidad.
        /// </summary> 
        /// <param name="view">
        ///    El objeto DataGridView que se va a manipular.
        /// </param>  
        /// <param name="rowIndex">
        ///    La posición de la fila a actualizar.
        /// </param>  
        /// <param name="entity">
        ///    Entidad que se usará para actualizar la fila de la vista.
        /// </param>
        /// <param name="drawBKColor">
        ///    Indica si se pintará el fonde de las celdas.
        /// </param>
        public static void UpdateRow(DataGridView view, int rowIndex, CAIN.Entity entity, bool drawBKColor = true)
        {   
            DataGridViewColumnCollection cols = view.Columns; 
            DataGridViewRow row = view.Rows[rowIndex];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                DataGridViewCell cell = row.Cells[i];

                /* Pintamos el fondo de las celdas */
                
                if (drawBKColor)
                {
                    if (entity.Track.Status != CAIN.Track.StatusTypes.Cataloged)
                        cell.Style.BackColor = Color.LightSalmon;
                    else
                    {
                        if (entity.Track.Reliability < 40)
                            cell.Style.BackColor = Color.LightSalmon;
                        else if (entity.Track.Reliability >= 40 && entity.Track.Reliability <= 80)
                            cell.Style.BackColor = Color.LightYellow;
                        else if (entity.Track.Reliability > 80)
                            cell.Style.BackColor = Color.LightGreen;
                    }
                }

                /* Cada celda contendrá la información que corresponda en función de la columna a la que pertenezca */

                switch (cols[i].HeaderText)
                {
                    case "Título":
                        cell.Value = entity.Track.Title;
                        break;
                    case "Duración":
                        cell.Value = entity.Track.Duration > 0 ? TimeSpan.FromSeconds(entity.Track.Duration).ToString(@"mm\:ss") : "--";
                        break;
                    case "Álbum":
                        cell.Value = entity.Album.Title;
                        break;
                    case "Año":
                        cell.Value = entity.Album.Year > 0 ? entity.Album.Year.ToString() : "--";
                        break;
                    case "Artista(s)":
                        cell.Value = String.Join(", ", entity.Artists.Select(item => item.ToString()));
                        break;
                    case "Estado":
                        cell.Value = CAIN.Utils.GetStringFromStatus(entity.Track.Status);
                        break;
                    default:
                        CAIN.Tag tag = entity.Tags.FirstOrDefault(item => item.Name == cols[i].HeaderText);
                        cell.Value = tag != null ? tag.Value : String.Empty;
                        break;
                }
            }
        }        
        
        /// <summary>
        ///    Método que permite ordenar el contenido de una vista en  la fila de una vista con la información de una entidad.
        /// </summary> 
        /// <param name="view">
        ///    El objeto DataGridView que se va a manipular.
        /// </param>  
        /// <param name="columnIndex">
        ///    La posición de la columna que se usará para la ordenación.
        /// </param>  
        /// <param name="entities">
        ///    Lista de entidades a ordenar.
        /// </param>
        public static void SortByColumn(DataGridView view, int columnIndex, ref List<CAIN.Entity> entities)
        {
            switch (view.Columns[columnIndex].HeaderText)
            {
                case "Título":
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => item.Track.Title).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => item.Track.Title).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;
                case "Duración":
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => item.Track.Duration).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => item.Track.Duration).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;
                case "Álbum":
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => item.Album.Title).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => item.Album.Title).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;
                case "Año":
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => item.Album.Year).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => item.Album.Year).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;
                case "Artista(s)":
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => String.Join(", ", item.Artists.Select(y => y.Name))).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => String.Join(", ", item.Artists.Select(y => y.Name))).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;
                case "Estado":
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => CAIN.Utils.GetStringFromStatus(item.Track.Status)).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => item.Track.Status).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;
                default:
                    if (DataGridViewCustomizer.OrderBy[columnIndex] != SortOrder.Ascending)
                    {
                        entities = entities.OrderBy(item => item.Tags.Exists(y => y.Name == view.Columns[columnIndex].HeaderText) ? item.Tags.First(y => y.Name == view.Columns[columnIndex].HeaderText).Name : String.Empty).ToList(); 
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Ascending;
                    }
                    else
                    {
                        entities = entities.OrderByDescending(item => item.Tags.Exists(y => y.Name == view.Columns[columnIndex].HeaderText) ? item.Tags.First(y => y.Name == view.Columns[columnIndex].HeaderText).Name : String.Empty).ToList();
                        DataGridViewCustomizer.OrderBy[columnIndex] = SortOrder.Descending;
                    }
                    break;                
            }
        } 

        /// <summary>
        ///    Método que permite obtener la lista de columnas de una vista.
        /// </summary> 
        /// <param name="view">
        ///    El objeto DataGridView que se va a manipular.
        /// </param>
        /// <returns>
        ///    La lista de columnas de la vista.
        /// </returns>
        public static List<string> GetColumns(DataGridView view)
        {
            List<string> columns = new List<string>();

            foreach (DataGridViewTextBoxColumn column in view.Columns)
                columns.Add(column.HeaderText);

            return columns;
        } 
        
        /// <summary>
        ///    Método que permiter llenar una vista a partir de una lista de entidades.
        /// </summary> 
        /// <param name="view">
        ///    El objeto DataGridView que se va a manipular.
        /// </param>  
        /// <param name="entities">
        ///    La lista de entidades que se usará para rellenar la vista.
        /// </param>
        /// <param name="drawBKColor">
        ///    Indica si se pintará el fonde de las celdas.
        /// </param>
        public static void Fill(DataGridView view, List<CAIN.Album> albums, List<string> artists)
        {
            /* Borramos todas las filas de la vista */

            view.Rows.Clear();

            DataGridViewColumnCollection cols = view.Columns;

            /* Para cada entidad, creamos una fila y rellenamos las celdas con la información relacionada */

            for (int j = 0; j < albums.Count; j++)
            //foreach (CAIN.Album album in albums)
            {                        
                int rowIndex = view.Rows.Add();

                DataGridViewRow row = view.Rows[rowIndex];

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    DataGridViewCell cell = row.Cells[i];

                    /* Cada celda contendrá la información que corresponda en función de la columna a la que pertenezca */

                    switch (cols[i].HeaderText)
                    {
                        case "Título":
                            cell.Value = albums[j].Title;
                            break;
                        case "Año":
                            cell.Value = albums[j].Year;
                            break;
                        case "Artista(s)":
                            cell.Value = artists[j];
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                }
            }
        }
    }
}

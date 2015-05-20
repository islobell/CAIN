using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CAINClient
{
    /// <summary>
    ///    Clase para el manejo de la información de los controles del panel izquierdo.
    /// </summary>
    public class EditionPanelValues
    {
        public string Title;           ///< Título de la canción
        public string Duration;        ///< Duración de la canción (en segundos)
        public string Album;           ///< Título del álbum      
        public string Year;            ///< Año del álbum      
        public Image Cover;            ///< Carátula del álbum
        public string Artists;         ///< Lista de artistas
        public List<CAIN.Tag> Tags;    ///< Lista de etiquetas

        /// <summary>
        ///    Constructor.
        /// </summary>
        public EditionPanelValues()
        {
            this.Title = String.Empty;
            this.Duration = String.Empty;
            this.Album = String.Empty;
            this.Year = String.Empty;    
            this.Cover = null;
            this.Artists = String.Empty;
            this.Tags = new List<CAIN.Tag>();
        }

        /// <summary>
        ///    Método que permite comprobar si los controles están vacíos (no contienen información).
        /// </summary>  
        /// <returns>
        ///    True, sino hay contenido. False, sino.
        /// </returns>
        public bool IsEmpty()
        {
            bool bEmpty = true;

            bEmpty &= String.IsNullOrEmpty(Title);
            bEmpty &= String.IsNullOrEmpty(Duration);
            bEmpty &= String.IsNullOrEmpty(Album);
            bEmpty &= String.IsNullOrEmpty(Year);   
            bEmpty &= this.Cover == null;
            bEmpty &= String.IsNullOrEmpty(Artists);
            bEmpty &= this.Tags.Count == 0;
                
            return bEmpty;
        } 
        
        /// <summary>
        ///    Método que permite comprobar si la información que contienen los controles es válida.
        /// </summary>   
        /// <returns>
        ///    True, si la misma información es válida. False, sino.
        /// </returns>
        //public bool IsValid(int count, out string text)
        //{
        //    text = String.Empty;
        //    StringBuilder message = new StringBuilder();

        //    if (count == 1 && String.IsNullOrEmpty(this.Title))
        //        message.Append(" · El campo 'Título' no puede estar vacío.\r\n");

        //    if (count == 1 || (count > 1 && !String.IsNullOrEmpty(this.Duration)))
        //    {
        //        int duration = CAIN.Utils.GetSeconds(CAIN.Utils.FormatSeconds(this.Duration));
        //        if (duration == 0)
        //            message.Append(" · El campo 'Duración' tiene que tener el formato '[m]m:ss'.\r\n");
        //    }

        //    if (count == 1 && String.IsNullOrEmpty(this.Album))
        //        message.Append(" · El campo 'Álbum' no puede estar vacío.\r\n");

        //    if (count == 1 || (count > 1 && !String.IsNullOrEmpty(this.Year)))
        //    {   
        //        int year;// = 0;               
        //        int currentYear = DateTime.Now.Year; 
        //        Int32.TryParse(this.Year, out year);
        //        if (count == 1 && (year < 1799 || year > currentYear))
        //            message.AppendFormat(" · El 'Año' tiene que ser un valor comprendido entre 1799 y {0}.\r\n", currentYear);
        //    }

        //    /*if (count == 1 && this.Cover != null)
        //        message.Append(" · El campo 'Carátula' no puede estar vacío.\r\n");*/

        //    if (count == 1 || (count > 1 && !String.IsNullOrEmpty(this.Artists)))
        //    {
        //        List<string> artists = this.Artists.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //        artists = artists.Where(item => !String.IsNullOrEmpty(item.Trim())).ToList();
        //        if (artists.Count == 0) 
        //            message.Append(" · El campo 'Artista(s)' no puede estar vacío.\r\n");
        //    }

        //    /*if (count > 1 && !String.IsNullOrEmpty(this.Tags))
        //        message.Append(" · El campo 'Etiquetas' no puede estar vacío.\r\n"); */

        //    if (message.Length > 0)
        //    {
        //        text = message.ToString();
        //        return false;
        //    }

        //    return true;
        //}

        /// <summary>
        ///    Método que permite comprobar si dos objetos de tipo <see cref="EditionPanelValues" /> contienen la misma información.
        /// </summary>   
        /// <returns>
        ///    True, si los objetos contienen la misma información. False, sino.
        /// </returns>
        public static bool Equals(EditionPanelValues values1, EditionPanelValues values2)
        {
            bool bEquals = true;

            bEquals &= String.Equals(values1.Title, values2.Title);
            bEquals &= String.Equals(values1.Duration, values2.Duration);
            bEquals &= String.Equals(values1.Album, values2.Album);
            bEquals &= String.Equals(values1.Year, values2.Year);
            bEquals &= CAIN.Utils.ImageEquals(values1.Cover, values2.Cover);
            bEquals &= CAIN.Artist.Equals(values1.Artists, values2.Artists);
            bEquals &= CAIN.Tag.Equals(values1.Tags, values2.Tags);
            
            return bEquals;
        }         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIN
{
    /// <summary>
    ///    Clase para almacenar la información resultante del proceso de resolución.
    /// </summary>
    public class Metadata
    {
        public int ResultIndex;                 ///< Posición en la lista de resultados (results) de AcoustID
        public int RecordingIndex;              ///< Posición en la lista de grabaciones (recordings) de AcoustID
        public int ReleaseGroupIndex;           ///< Posición en la lista de álbumes (releaseGroups) de AcoustID
        public float Score;                     ///< Puntuación (fiabilidad) asignada por AcoustID
        public float Probability;               ///< Probabilidad (grado de ocurrencia) que tiene la información
        public bool Selected;                   ///< Indica si la información ha sido seleccionada
        public Entity Entity;                   ///< Objeto de tipo <see cref="Entity" /> 

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Metadata()
        {
            this.ResultIndex = -1;
            this.RecordingIndex = -1;
            this.ReleaseGroupIndex = -1;
            this.Score = 0.0f;      
            this.Probability = 0.0f;
            this.Selected = false;  
            this.Entity = new Entity();
        }

        /// <summary>
        ///    Método que permite comprobar si la información es válida.
        /// </summary>
        /// <returns>
        ///    True, si la información es válida. False, sino.
        /// </returns>
        public bool IsValid()
        {
            if (this.ResultIndex == -1 ||
                this.RecordingIndex == -1 ||
                this.ReleaseGroupIndex == -1 ||
                !this.Entity.IsValid())
                return false;
            else
                return true;
        }
    } 
}

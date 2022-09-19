using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueLab.Uteis.Bases
{
    [DebuggerDisplay("Id={Id}")]
    [Serializable]
    public class EntityBase
    {
        [System.Xml.Serialization.XmlIgnore]
        [NotMapped]
        public String Key
        {
            get
            {
                if (Id == 0)
                    return "";
                else return Id.ToString().EncryptAES().UrlEncode();
            }
            set
            {
                int id;
                if (!String.IsNullOrEmpty(value) &&
                    int.TryParse(value.UrlDecode().DecryptAES(), out id))
                    Id = id;
                else
                    Id = 0;
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public int Id { get; set; }
        public Boolean Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
        [NotMapped]
        public int NumeroLinhas { get; set; }
    }
}

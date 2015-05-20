using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIN
{
    public class Tag
    { 
        public long ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Tag()
        {
            this.ID = 0;
            this.Name = String.Empty;
            this.Value = String.Empty;
        }

        public Tag(string Name, string Value)
        {
            this.ID = 0;
            this.Name = Name;
            this.Value = Value;
        }

        public bool IsValid()
        {
            bool bValid = true;

            bValid &= !String.IsNullOrEmpty(this.Name);
            bValid &= !String.IsNullOrEmpty(this.Value);

            return bValid;
        }

        public static bool Equals(Tag tag1, Tag tag2)
        {
            bool bEqual = true;

            bEqual &= String.Equals(tag1.Name, tag2.Name);//.ToUpper(), tag2.Name.ToUpper()); 
            bEqual &= String.Equals(tag1.Value, tag2.Value);//.ToUpper(), tag2.Value.ToUpper());

            return bEqual;
        }

        public static bool Equals(List<Tag> tags1, List<Tag> tags2)
        {
            bool bEqual = true;

            if (tags1.Count != tags2.Count)
                return false;

            for (int i = 0; i < tags1.Count && bEqual; i++)
                bEqual = Tag.Equals(tags1[i], tags2[i]); 

            return bEqual;
        }
    }
}

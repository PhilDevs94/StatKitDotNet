using DotNetCoreStartKit.Ultilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreStartKit.Core.Model
{
    public class Entity
    {
        public Entity()
        {    
           Id = GuidComb.GenerateComb();  
        }
        public Guid Id { get; set; }
        public DateTime CreatDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Delete { get; set; }
        public virtual ApplicationUser UserAccount { get; set; }
    }
}

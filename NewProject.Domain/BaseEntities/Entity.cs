using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.BaseEntities
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
        protected Entity()
        {

        }

        protected Entity(TKey id)
        {
            Id = id;
        }
    }
}

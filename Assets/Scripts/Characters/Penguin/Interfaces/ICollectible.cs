using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.Penguin.Interfaces
{
    public interface ICollectible
    {
        public void Accept(IVisitor visitor);
    }
}

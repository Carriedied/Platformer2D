using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.Penguin.Interfaces
{
    internal interface ICollectible
    {
        void Accept(IVisitor visitor);
    }
}

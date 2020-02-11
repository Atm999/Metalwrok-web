using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NavTree
    {
        public string name { get; set; }
        public IList<NavTree> children { get; set; } = new List<NavTree>();

        public void Addchildren(NavTree node)
        {
            this.children.Add(node);
        }
    }
}

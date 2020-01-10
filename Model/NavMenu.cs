using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NavMenu
    {
        public string Name { get; set; }
        public string TemplateUrl { get; set; }
        public string Icon { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<NavMenu> SubNavMenus { get; set; }
    }

    /// <summary>
    /// 左侧导航菜单视图模型
    /// </summary>
    public class NavBarMenus
    {
        public IList<NavMenu> NavMenus { get; set; }
    }
}

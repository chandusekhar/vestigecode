using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.UI;

namespace CoreComponent.BusinessObjects
{
    public class ModifyTabControl
    {
        //Removing TabPages from TabControl
        public void RemoveTabPage(TabControl currentTabControl, TabPage currentTabPage)
        {
            currentTabControl.TabPages.Remove(currentTabPage);
        }
        public void RemoveTabPage(TabControl currentTabControl, System.Int32 index)
        {
            currentTabControl.TabPages.RemoveAt(index);
        }
        public void RemoveTabPage(TabControl currentTabControl, System.String key)
        {
            currentTabControl.TabPages.RemoveByKey(key);
        }
        public void RemoveTabPageArray(TabControl currentTabControl, System.Int32 startIndex)
        {
            do
            {
                RemoveTabPage(currentTabControl, startIndex);
            }
            while (currentTabControl.TabPages.Count > startIndex);
        }

        //Adding TabPages to TabControl
        public void AddTabPage(TabControl currentTabControl, TabPage currentTabPage)
        {
            currentTabControl.TabPages.Add(currentTabPage);
        }
        public void AddTabPage(TabControl currentTabControl, System.String value)
        {
            currentTabControl.TabPages.Add(value);
        }
        public void AddTabPage(TabControl currentTabControl, System.String key, System.String value)
        {
            currentTabControl.TabPages.Add(key, value);
        }
        public void AddTabPageArray(TabControl currentTabControl, TabPage[] tabPages)
        {
            currentTabControl.TabPages.AddRange(tabPages);
        }

        //Inserting TabPages to TabControl
        public void AddTabPage(TabControl currentTabControl, TabPage currentTabPage, System.Int32 index)
        {
            InsertTab(currentTabControl, currentTabPage, index);
        }
        public void AddTabPage(TabControl currentTabControl, System.String value, System.Int32 index)
        {
            TabPage customTabPage = MakeTabPage(value);
            InsertTab(currentTabControl, customTabPage, index);
        }
        public void AddTabPage(TabControl currentTabControl, System.String key, System.String value, System.Int32 index)
        {
            TabPage customTabPage = MakeTabPage(key, value);
            InsertTab(currentTabControl, customTabPage, index);
        }

        //Custom Insertion of TabPage
        private void InsertTab(TabControl currentTabControl, TabPage currentTabPage, System.Int32 index)
        {
            AddTabPage(currentTabControl, currentTabPage);

            for (int currentIndex = currentTabControl.TabPages.Count - 1; currentIndex >= index; --currentIndex)
            {
                currentTabControl.TabPages[currentIndex] = currentTabControl.TabPages[currentIndex - 1];
            }
            currentTabControl.TabPages[index] = currentTabPage;
        }

        //Making TabPage
        private TabPage MakeTabPage(string value)
        {
            return MakeTabPage(value, value);
        }
        private TabPage MakeTabPage(string key, string value)
        {
            TabPage tabPage = new TabPage();
            tabPage.Name = key;
            tabPage.Text = value;

            return tabPage;
        }
    }
}

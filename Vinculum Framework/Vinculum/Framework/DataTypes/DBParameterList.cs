namespace Vinculum.Framework.DataTypes
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class DBParameterList
    {
        private List<DBParameter> parameterList;

        public DBParameterList()
        {
            this.parameterList = new List<DBParameter>();
        }

        public DBParameterList(IEnumerable<DBParameter> collection)
        {
            this.parameterList = new List<DBParameter>(collection);
        }

        public void Add(DBParameter item)
        {
            this.parameterList.Add(item);
        }

        public void AddRange(IEnumerable<DBParameter> collection)
        {
            this.parameterList.AddRange(collection);
        }

        public void Clear()
        {
            this.parameterList.Clear();
        }

        public int Count
        {
            get
            {
                return this.parameterList.Count;
            }
        }

        public DBParameter this[string name]
        {
            get
            {
                for (int index = 0; index < this.parameterList.Count; index++)
                {
                    DBParameter var0000 = this.parameterList[index];
                    if (var0000.Name.Trim().ToLower().CompareTo(name.Trim().ToLower()) == 0)
                    {
                        return this.parameterList[index];
                    }
                }
                throw new IndexOutOfRangeException("key not found");
            }
            internal set
            {
                bool bKeyFound = false;
                for (int index = 0; index < this.parameterList.Count; index++)
                {
                    DBParameter var0000 = this.parameterList[index];
                    if (var0000.Name.Trim().ToLower().CompareTo(name.Trim().ToLower()) == 0)
                    {
                        this.parameterList.Insert(index, value);
                        this.parameterList.RemoveAt(index + 1);
                        bKeyFound = true;
                        break;
                    }
                }
                if (!bKeyFound)
                {
                    throw new IndexOutOfRangeException("key not found");
                }
            }
        }

        public DBParameter this[int index]
        {
            get
            {
                return this.parameterList[index];
            }
            internal set
            {
                this.parameterList.Insert(index, value);
                this.parameterList.RemoveAt(index + 1);
            }
        }
    }
}


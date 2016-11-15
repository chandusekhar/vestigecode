using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace POSClient.UI.Controls
{
	internal interface IItemSelectable
	{
		MethodInfo Action { get; }

		object[] Args { get; }

		object DataInstance { get; }

		Image DisplayImage { get; }

		string DisplayText { get; }

		string ParentID { get; }

		string ID { get; }
	}

	public class SelectableItem : IItemSelectable
	{
		#region Member Variables

		private object m_instance;

		private string m_text, m_id, m_parent;
		private Image m_image;
		private MethodInfo m_action;
		private object[] m_args;

		#endregion

		#region Constructor

		internal SelectableItem(string id, string parent, string text, Image image, MethodInfo action, object instance, params object[] args)
		{
			m_id = id;
			m_parent = parent;
			m_text = text;
			m_image = image;
			m_action = action;
			m_args = args;
			m_instance = instance;
		}

		#endregion

		#region Properties

		#region IItemSelectable Members

		public MethodInfo Action { get { return m_action; } }

		public object[] Args { get { return m_args; } }

		public object DataInstance { get { return m_instance; } }

		public Image DisplayImage { get { return m_image; } }

		public string DisplayText { get { return m_text; } }

		public string ParentID { get { return m_parent; } }

		public string ID { get { return m_id; } }

		#endregion

		#endregion

		#region Methods

		public static Dictionary<int, List<SelectableItem>> Load<T>(IEnumerable<T> selectableitems, string idField, string parentField, string textField, string imageField, string actionField, string childField, string argsField)
		{
			Type iType = typeof(T);
			PropertyInfo iProp;
			List<SelectableItem> litem;
			Dictionary<int, List<SelectableItem>> dcoll = new Dictionary<int, List<SelectableItem>>();

			if (selectableitems == null) return dcoll;

			IList<T> collection = new List<T>(selectableitems);
			idField = idField.Trim();
			parentField = parentField.Trim();
			textField = textField.Trim();
			childField = childField.Trim();
			imageField = imageField.Trim();
			actionField = actionField.Trim();
			argsField = argsField.Trim();

			for (int index = 0; index < collection.Count; index++)
			{
				Image iImage = null;

				if (imageField.Length > 0)
				{
					iProp = iType.GetProperty(imageField);

					if (iProp.PropertyType.Equals(typeof(string)))
						iImage = File.Exists(iProp.GetValue(collection[index], null).ToString()) ? Image.FromFile(iProp.GetValue(collection[index], null).ToString()) : null;
					else if (iProp.PropertyType.Equals(typeof(IntPtr)))
						iImage = iProp.GetValue(collection[index], null) != null ? Image.FromHbitmap((IntPtr)iProp.GetValue(collection[index], null)) : null;
					else if (iProp.PropertyType.Equals(typeof(Stream)))
						iImage = iProp.GetValue(collection[index], null) != null ? Image.FromStream(iProp.GetValue(collection[index], null) as Stream) : null;
				}

				SelectableItem item = new SelectableItem(iType.GetProperty(idField).GetValue(collection[index], null).ToString(),
														string.Empty,
														textField.Length > 0 ? iType.GetProperty(textField).GetValue(collection[index], null).ToString() : string.Empty,
														iImage,
														actionField.Length > 0 ? iType.GetMethod(actionField) : null,
														collection[index],
														argsField.Length > 0 ? iType.GetProperty(argsField).GetValue(collection[index], null) : null);
				if (childField.Length > 0)
					PrepareCollection<T>(1, ref dcoll, item.ID,
									new List<T>((iType.GetProperty(childField).GetValue(collection[index], null) as IEnumerable<T>)),
									idField, parentField, textField, imageField, actionField, childField, argsField);
				
				if (dcoll.ContainsKey(0))
					dcoll[0].Add(item);
				else
				{
					litem = new List<SelectableItem>();
					litem.Add(item);
					dcoll.Add(0, litem);
				}
			}
			return dcoll;
		}

		private static void PrepareCollection<T>(int level, ref Dictionary<int, List<SelectableItem>> dcoll, string parent, List<T> collection, string idField, string parentField, string textField, string imageField, string actionField, string childField, string argsField)
		{
			Type iType = typeof(T);
			PropertyInfo iProp;
			List<SelectableItem> litem;

			for (int index = 0; index < collection.Count; index++)
			{
				Image iImage = null;

				if (imageField.Length > 0)
				{
					iProp = iType.GetProperty(imageField);

					if (iProp.PropertyType.Equals(typeof(string)))
						iImage = File.Exists(iProp.GetValue(collection[index], null).ToString()) ? Image.FromFile(iProp.GetValue(collection[index], null).ToString()) : null;
					else if (iProp.PropertyType.Equals(typeof(IntPtr)))
						iImage = iProp.GetValue(collection[index], null) != null ? Image.FromHbitmap((IntPtr)iProp.GetValue(collection[index], null)) : null;
					else if (iProp.PropertyType.Equals(typeof(Stream)))
						iImage = iProp.GetValue(collection[index], null) != null ? Image.FromStream(iProp.GetValue(collection[index], null) as Stream) : null;
				}

				SelectableItem item = new SelectableItem(iType.GetProperty(idField).GetValue(collection[index], null).ToString(),
														parent,
														textField.Length > 0 ? iType.GetProperty(textField).GetValue(collection[index], null).ToString() : string.Empty,
														iImage,
														actionField.Length > 0 ? iType.GetMethod(actionField) : null,
														collection[index],
														argsField.Length > 0 ? iType.GetProperty(argsField).GetValue(collection[index], null) : null);
				if (childField.Length > 0)
					PrepareCollection(level + 1, ref dcoll, item.ID,
									new List<T>((iType.GetProperty(childField).GetValue(collection[index], null) as IEnumerable<T>)),
									idField, parentField, textField, imageField, actionField, childField, argsField);

				if (dcoll.ContainsKey(level))
					dcoll[level].Add(item);
				else
				{
					litem = new List<SelectableItem>();
					litem.Add(item);
					dcoll.Add(level, litem);
				}
			}
		}

		#endregion
	}
}

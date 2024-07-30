using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace AlexPacientes.Models.AppModels
{
    public class DayEventCollection<T> : ICollection
    {
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public ObservableCollection<T> Items { get; set; }

        public int Count => Items.Count;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => Items;

        public DayEventCollection()
        {
            Items = new ObservableCollection<T>();
        }
        
        public DayEventCollection(Color Color1, Color Color2)
        {
            this.Color1 = Color1;
            this.Color2 = Color2;
            Items = new ObservableCollection<T>();
        }

        public void CopyTo(Array array, int index)
        {

        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

    }

}

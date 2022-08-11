using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TaffPizza.Models;

namespace TaffPizza
{
    static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection, int indexer = 0) where T : IComparable
        {
            List<T> list = collection.ToList();
            switch (indexer)
            {
                case 0:
                    {
                        list.Sort((p1, p2) =>
                        {
                            return (p1 as Pizza).Nom.CompareTo(
                                (p2 as Pizza).Nom
                                );
                        });
                    }
                    break;
                case 1:
                    {
                        list.Sort((p1, p2) =>
                        {
                            return (p1 as Pizza).Nom.CompareTo(
                                (p2 as Pizza).Nom
                                );
                        });
                    }
                    break;
                case 2:
                    {
                        list.Sort((p2, p1) =>
                        {
                            return (p1 as Pizza).Prix.CompareTo(
                                (p2 as Pizza).Prix
                                );
                        });
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (!(list[i] as Pizza).IsFavourite)
                            {
                                list.RemoveAt(i);
                            }
                        }
                    }
                    break;
            }
            if(indexer != 3)
            {
                for (int i = 0; i < list.Count(); i++)
                    collection.Move(collection.IndexOf(list[i]), i);
            }
        }
    }
}

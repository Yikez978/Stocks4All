﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yolo.CustomControls;

namespace Stocks4All.CustomControls
{
  public class ThreadedBindingList<T> : BindingList<T>
  {
    SynchronizationContext ctx = SynchronizationContext.Current;
    public String sortProperty;
    public ListSortDirection sortDirection;

    public ThreadedBindingList()
      : base()
    {
      sortDirection = ListSortDirection.Ascending;
      //base.((BindingList)list);
    }
    public ThreadedBindingList(List<T> list) : base(list)
    {
      //base.((BindingList)list);
    }
    protected override void OnAddingNew(AddingNewEventArgs e)
    {

      if (ctx == null)
      {
        BaseAddingNew(e);
      }
      else
      {
        ctx.Send(delegate
        {
          BaseAddingNew(e);
        }, null);
      }
    }
    void BaseAddingNew(AddingNewEventArgs e)
    {
      base.OnAddingNew(e);
    }

    //protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
    //{
    //  List<T> itemsList = (List<T>)this.Items;
    //  if (property.PropertyType.GetInterface("IComparable") != null)
    //  {
    //    itemsList.Sort(new Comparison<T>(delegate(T x, T y)
    //    {
    //      // Compare x to y if x is not null. If x is, but y isn't, we compare y
    //      // to x and reverse the result. If both are null, they're equal.
    //      if (property.GetValue(x) != null)
    //        return ((IComparable)property.GetValue(x)).CompareTo(property.GetValue(y)) * (direction == ListSortDirection.Descending ? -1 : 1);
    //      else if (property.GetValue(y) != null)
    //        return ((IComparable)property.GetValue(y)).CompareTo(property.GetValue(x)) * (direction == ListSortDirection.Descending ? 1 : -1);
    //      else
    //        return 0;
    //    }));
    //  }

    //  isSorted = true;
    //  sortProperty = property;
    //  sortDirection = direction;
    //}

    protected override void OnListChanged(ListChangedEventArgs e)
    {
      // SynchronizationContext ctx = SynchronizationContext.Current;
      if (ctx == null)
      {
        BaseListChanged(e);
      }
      else
      {
        ctx.Send(delegate
        {
          BaseListChanged(e);
        }, null);
      }
    }
    void BaseListChanged(ListChangedEventArgs e)
    {
      base.OnListChanged(e);
    }
  } 
}

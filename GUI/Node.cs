/*
        This program is free software: you can redistribute it and/or modify
        it under the terms of the GNU General Public License as published by
        the Free Software Foundation, either version 3 of the License, or
        (at your option) any later version.

        This program is distributed in the hope that it will be useful,
        but WITHOUT ANY WARRANTY; without even the implied warranty of
        MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.    See the
        GNU General Public License for more details.

        You should have received a copy of the GNU General Public License
        along with this program.    If not, see <http://www.gnu.org/licenses/>.
    
        Copyright (C) 2012 Andrey Mushatov ( openPowerCfg@gmail.com )
*/


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Aga.Controls.Tree;

namespace OpenPowerCfg.GUI 
{
    public class Node 
    {
        private TreeModel treeModel;
        private Node parent;
        private NodeCollection nodes;
        protected List<Node> childs = new List<Node>();

        private string text;
        private Image image;
        private bool visible;

        private TreeModel RootTreeModel() 
        {
            Node node = this;
            while (node != null) 
            {
                if (node.Model != null)
                    return node.Model;
                node = node.parent;
            }
            return null;
        }

        public Node() : this(string.Empty) { }

        public Node(string text) 
        {
            this.text = text;
            this.nodes = new NodeCollection(this);
            this.visible = true;
        }

        public TreeModel Model 
        {
            get { return treeModel; }
            set { treeModel = value; }
        }

        public Node Parent 
        {
            get { return parent; }
            set {
                if (value != parent) 
                {
                    if (parent != null)
                        parent.nodes.Remove(this);
                    if (value != null)
                        value.nodes.Add(this);
                }
            }
        }

        public Collection<Node> Nodes 
        {
            get { return nodes; }
        }

        public List<Node> Childs
        {
            get
            {
                if (0 == this.childs.Count) // TODO: Change with IsLoaded or similar
                {
                    LoadChilds();
                }
                return this.childs;
            }
        }

        protected virtual void LoadChilds()
        {
        }


        public virtual string Text 
        {
            get { return text; }
            set 
            {
                if (text != value) 
                {
                    text = value;
                }
            }
        }

        public Image Image 
        {
            get { return image; }
            set 
            {
                if (image != value) 
                {
                    image = value;
                }
            }
        }

        public virtual bool IsVisible 
        {
            get { return visible; }
            set 
            {
                if (value != visible) 
                {
                    visible = value;                    
                    TreeModel model = RootTreeModel();
                    if (model != null && parent != null) 
                    {
                        int index = 0;
                        for (int i = 0; i < parent.nodes.Count; i++) 
                        {
                            Node node = parent.nodes[i];
                            if (node == this)
                                break;
                            if (node.IsVisible || model.ForceVisible)
                                index++;
                        }
                        if (model.ForceVisible) 
                        {
                                model.OnNodeChanged(parent, index, this);
                        } 
                        else 
                        {                            
                            if (value)
                                model.OnNodeInserted(parent, index, this);
                            else
                                model.OnNodeRemoved(parent, index, this);
                        }
                    }
                    if (IsVisibleChanged != null)
                        IsVisibleChanged(this);
                }
            }
        }

        public virtual String Description
        {
            get { return ""; }
        }

        public delegate void NodeEventHandler(Node node);

        public event NodeEventHandler IsVisibleChanged;
        public event NodeEventHandler NodeAdded;
        public event NodeEventHandler NodeRemoved;

        private class NodeCollection : Collection<Node> 
        {
            private Node owner;

            public NodeCollection(Node owner) 
            {
                this.owner = owner;
            }

            protected override void ClearItems() 
            {
                while (this.Count != 0)
                    this.RemoveAt(this.Count - 1);
            }

            protected override void InsertItem(int index, Node item) 
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                if (item.parent != owner) 
                {
                    if (item.parent != null)
                        item.parent.nodes.Remove(item);
                    item.parent = owner;
                    base.InsertItem(index, item);

                    TreeModel model = owner.RootTreeModel();
                    if (model != null)
                        model.OnStructureChanged(owner);
                    if (owner.NodeAdded != null)
                        owner.NodeAdded(item);
                }
            }

            protected override void RemoveItem(int index) 
            {
                Node item = this[index];
                item.parent = null;
                base.RemoveItem(index);

                TreeModel model = owner.RootTreeModel();
                if (model != null) 
                    model.OnStructureChanged(owner);
                if (owner.NodeRemoved != null)
                    owner.NodeRemoved(item);
            }

            protected override void SetItem(int index, Node item) 
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                RemoveAt(index);
                InsertItem(index, item);
            }
        }

    }
}

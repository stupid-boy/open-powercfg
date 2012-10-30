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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aga.Controls.Tree;

namespace OpenPowerCfg.GUI 
{
    public class TreeModel : ITreeModel 
    {
        private Node root;
        private bool forceVisible = false;

        public TreeModel() 
        {
            root = null;
        }

        public Node Root
        {
            set { this.root = value; }
        }

        public TreePath GetPath(Node node) 
        {
            if (node == root)
            {
                return TreePath.Empty;
            }
            else
            {
                Stack<object> stack = new Stack<object>();
                while (node != root)
                {
                    stack.Push(node);
                    node = node.Parent;
                }
                return new TreePath(stack.ToArray());
            }
        }

        public Collection<Node> Nodes 
        {
            get { return root.Nodes; }
        }

        private Node GetNode(TreePath treePath) 
        {
            Node parent = root;
            foreach (object obj in treePath.FullPath) 
            {
                Node node = obj as Node;
                if (node == null || node.Parent != parent)
                    return null;
                parent = node;
            }
            return parent;
        }

        public IEnumerable GetChildren(TreePath treePath) 
        {                
            if (treePath.IsEmpty())
            {
                yield return root;
            }
            else
            {
                Node node = treePath.LastNode as Node;
                if (node != null)
                {
                    if (0 != node.Childs.Count)
                    {
                        foreach (Node n in node.Childs)
                            if (forceVisible || n.IsVisible)
                                yield return n;
                    }
                    else
                    {
                     // yield break;
                    }
                }
                else
                {
                    yield break;
                }
            }
        }

        public bool IsLeaf(TreePath treePath) 
        {
            return (treePath.LastNode is SettingNode);
        }

        public bool ForceVisible 
        {
            get {return forceVisible;}
            set 
            {
                if (value != forceVisible) 
                {
                    forceVisible = value;
                    OnStructureChanged(root);
                }
            }
        }

        #pragma warning disable 67
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreePathEventArgs> StructureChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        #pragma warning restore 67

        public void OnNodeChanged(Node parent, int index, Node node) 
        {
            if (NodesChanged != null && parent != null) 
            {
                TreePath path = GetPath(parent);
                if (path != null) 
                    NodesChanged(this, new TreeModelEventArgs(
                        path, new int[] { index }, new object[] { node }));
            }
        }

        public void OnStructureChanged(Node node) 
        {
            if (StructureChanged != null)
                StructureChanged(this,
                    new TreeModelEventArgs(GetPath(node), new object[0]));
        }

        public void OnNodeInserted(Node parent, int index, Node node) 
        {
            if (NodesInserted != null) 
            {
                TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent),
                    new int[] { index }, new object[] { node });
                NodesInserted(this, args);
            }

        }

        public void OnNodeRemoved(Node parent, int index, Node node) 
        {
            if (NodesRemoved != null) 
            {
                TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), 
                    new int[] { index }, new object[] { node });
                NodesRemoved(this, args);
            }
        }
    }
}

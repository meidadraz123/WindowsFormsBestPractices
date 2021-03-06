﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp
{
    public partial class SubscriptionView : UserControl, ISubscriptionView
    {
        public SubscriptionView()
        {
            InitializeComponent();
            treeViewPodcasts.AfterSelect += (s, a) => OnSelectionChanged();
        }

        public TreeNode SelectedNode
        {
            get
            {
                return treeViewPodcasts.SelectedNode;
            }
        }

        public event EventHandler SelectionChanged;

        protected virtual void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddNode(TreeNode treeNode)
        {
            treeViewPodcasts.Nodes.Add(treeNode);
        }

        public void RemoveNode(string key)
        {
            var node = treeViewPodcasts.Nodes[key];
            treeViewPodcasts.Nodes.Remove(node);
        }

        public void SelectNode(string key)
        {
            treeViewPodcasts.SelectedNode = treeViewPodcasts.Nodes.Find(key, true)[0];
        }

        public bool IsEmpty()
        {
            return treeViewPodcasts.Nodes.Count == 0;
        }
    }

    public interface ISubscriptionView
    {
        TreeNode SelectedNode { get; }

        void AddNode(TreeNode treeNode);
        void RemoveNode(string key);
        void SelectNode(string key);
        bool IsEmpty();

        event EventHandler SelectionChanged;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab2
{
    internal class OcAvgNode
    {
        private Tuple<int, int> depthInterval = new Tuple<int, int>(0, 7);
        public Tuple<int, int> DepthInterval { get => depthInterval; }
        public OcAvgNode[] children= new OcAvgNode[8];
        //Some useful variables for the structure.
        public int depth;
        public bool isLeaf;
        public int[] HowDeepDoesBranchXGo = new int[8];
        int uniqueColorCount;

        //The main parameters we care about:
        public int sumRed = 0;
        public int sumGreen = 0;
        public int sumBlue = 0;
        public int pixelCount = 0;

        //colors??
        public static int uniqueColorsInTree = 0;
        public static void resetUniqueColorsInTree()
        {
            uniqueColorsInTree = 0;
        }
        //Constructor:
        public OcAvgNode(int _depth)
        {
            depth = _depth;
            if (depth==DepthInterval.Item2)
            {
                isLeaf = true;
            }
            else
            {
                isLeaf = false;
            }
        }
        public Color GetNodeColor()
        {
            return Color.FromArgb(sumRed / pixelCount, sumGreen / pixelCount, sumBlue / pixelCount);
        }

        public int GetChildTreeUniqueColorCount()
        {
            int sum = 0;
            if (isLeaf)
            {
                sum += 1;
            }
            else
            {
                foreach (var node in this.children)
                {
                    if (node != null)
                    {
                        sum += node.GetChildTreeUniqueColorCount();
                    }
                }
            }
            return sum;
        }

        public void AddPixel(Color col)
        {
            pixelCount++;
            sumRed += col.R;
            sumGreen += col.G;
            sumBlue += col.B; 
        }
    }
    internal class OctreeAverageQuantization
    {
        public OcAvgNode root;

        public OctreeAverageQuantization()
        {
            root = new OcAvgNode(0);
            root.depth = -1;
            //initializeChildrenNodesRecursively(ref root,0);
        }

        public int insert(Color col)
        {
           int howDeepDoesItGo= _insert(ref root, col, 0);
            //return root.GetChildTreeUniqueColorCount();
            return GetUniqueColorCount();
        }

        public Color GetQuantizedColor(Color col)
        {
            return _GetQuantizedColor(ref root, col, 0);
        }
        public int GetUniqueColorCount()
        {
            //return root.GetChildTreeUniqueColorCount();
            return OcAvgNode.uniqueColorsInTree;
        }

        public void RemoveExtraColors(int limit)
        {
            if(limit<8)
            {
                limit = 8;
            }
            List<int> howDeepBranchX = root.HowDeepDoesBranchXGo.ToList();
            while (limit < OcAvgNode.uniqueColorsInTree)
            {
                int deepestDepth = root.HowDeepDoesBranchXGo.Max();
                if (deepestDepth <= 0)
                    return;
                int indexRootDeepestBranch = howDeepBranchX.IndexOf(deepestDepth);
                _RemoveExtraColors(ref root.children[indexRootDeepestBranch], indexRootDeepestBranch);
                root.HowDeepDoesBranchXGo[indexRootDeepestBranch]--;
                howDeepBranchX[indexRootDeepestBranch]--;
            }
            return;
        }

        //Private methods:

        private int _insert(ref OcAvgNode node, Color col, int depth)
        {
            int howDeepDoesItGo;
            if (node.isLeaf)
            {
                node.AddPixel(col);
                howDeepDoesItGo = node.depth;
            }
            else
            {
                node.AddPixel(col);
                int branchIndex = BranchSelector(col, depth); //branchIndex is basically the index of child in the children array.
                ref OcAvgNode childDestination = ref node.children[branchIndex];
                if (childDestination == null)
                {
                    node.children[branchIndex] = new OcAvgNode(depth);
                    if (node.children[branchIndex].isLeaf)
                    {
                        OcAvgNode.uniqueColorsInTree++;
                    }
                    childDestination = ref node.children[branchIndex];
                }
                howDeepDoesItGo = _insert(ref childDestination, col, depth + 1);
                node.HowDeepDoesBranchXGo[branchIndex] = howDeepDoesItGo; //- depth;
            }
            return howDeepDoesItGo;
        }

        //private static readonly Byte[] Mask = new Byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
        private int BranchSelector(Color col, int depth)
        {
            //int level = depth;
            //     return ((col.R & Mask[level]) == Mask[level] ? 4 : 0) |
            //((col.G & Mask[level]) == Mask[level] ? 2 : 0) |
            //((col.B & Mask[level]) == Mask[level] ? 1 : 0);
            int branchIndex;
            branchIndex =
            (GetBit(col.R, depth) ? 4 : 0)
            | (GetBit(col.G, depth) ? 2 : 0)
            | (GetBit(col.B, depth) ? 1 : 0); //GetBit returns "true" instead of 1 and "false" instead of 0 so we don't need to write GetBit(...)==1 explcitly
            return branchIndex;
        }
        private bool GetBit(byte b, int depthInTree)
        {
            int bitPosition = 7 - depthInTree;
            return (b & (1 << bitPosition)) != 0;
        }


        private void _RemoveExtraColors(ref OcAvgNode node, int rootMaxDepthBranchIndex)
        {
            //new and shiny method:
            int maxDepthIndex = 0;
            for (int i = 1; i <= node.DepthInterval.Item2; i++)
            {
                if (node.HowDeepDoesBranchXGo[maxDepthIndex] < node.HowDeepDoesBranchXGo[i])
                {
                    maxDepthIndex = i;
                }
            }
            if(node.children[maxDepthIndex].isLeaf) //max depth child = leaf means every child = leaf
            {
                OcAvgNode.uniqueColorsInTree -= node.GetChildTreeUniqueColorCount() - 1; //subtract one because one leaf gets added back in form of the parent.
                node.isLeaf = true;
            }
            else
            {
                _RemoveExtraColors(ref node.children[maxDepthIndex], rootMaxDepthBranchIndex);
                node.HowDeepDoesBranchXGo[maxDepthIndex]--;
            }
            return;
            ////old method
            //int uniqueColorsRemoved;
            //if (node.isLeaf && node.depth != 0)
            //    return 0;
            //if (node.depth == root.HowDeepDoesBranchXGo[rootMaxDepthBranchIndex]-2) //-2 as we stop one step before leaf and because first layer of nodes is considered levl 0
            //{
            //    uniqueColorsRemoved = node.GetChildTreeUniqueColorCount() - 1; //subtract one because one leaf gets added back in form of the parent.
            //    node.isLeaf = true;
            //    return uniqueColorsRemoved;
            //}
            //else
            //{
            //    int maxDepthIndex = 0;
            //    for (int i = 1; i <= node.DepthInterval.Item2; i++)
            //    {
            //        if (node.HowDeepDoesBranchXGo[maxDepthIndex] < node.HowDeepDoesBranchXGo[i])
            //        {
            //            maxDepthIndex = i;
            //        }
            //    }
            //    uniqueColorsRemoved = _RemoveExtraColors(ref node.children[maxDepthIndex], rootMaxDepthBranchIndex);
            //    node.HowDeepDoesBranchXGo[maxDepthIndex]--; //removing 1 from depth because one leaf in this branch was just removed
            //}
            //return uniqueColorsRemoved;
        }
        private Color _GetQuantizedColor(ref OcAvgNode node, Color col, int depth)
        { 
            if(node.isLeaf)
            {
                return node.GetNodeColor();
            }
            else
            {
                int branchIndex = BranchSelector(col, depth);
                ref OcAvgNode childDestination = ref node.children[branchIndex];
                return _GetQuantizedColor(ref childDestination, col, depth + 1);
            }
        }
    }
}

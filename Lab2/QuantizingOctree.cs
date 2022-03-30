using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphics_1.Lab2
{
    public class QuantOcNode
    {
        public QuantOcNode parent;
        //public List<QuantOcNode> children=new List<QuantOcNode>();
        public QuantOcNode[] children = new QuantOcNode[8];
        public int depth;

        private static readonly Byte[] Mask = new Byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

        //The root color RGB:
        public int red=0;
        public int green=0;
        public int blue=0;

        public Color GetNodeColor()
        {
            return Color.FromArgb(red / pixelCount, green / pixelCount, blue / pixelCount);
        }

        //Number of times root count is repeated and its pallete index.
        public int pixelCount=0;
        private int paletteIndex;
        
        public Boolean isLeaf
        {
            //get { return this.pixelCount > 0; }
            get { return this.children.SkipWhile(x => x == null).Count() == 0; }
        }
        public int GetChildTreeColorCount()
        {
            int sum = 0;
            if (isLeaf)
            {
                sum += 1;
                return sum;
            }
            foreach (var node in this.children)
            {
                if(node!=null)
                {
                    sum += node.GetChildTreeColorCount();
                }
            }
            return sum;
        }

        public int AddPixel(Color col)
        {
            pixelCount++;
            red += col.R; //should it be just =?
            green += col.G; //should it be just =?
            blue += col.B; //should it be just =?
            return 1;//change in total //needs to be fixed somehow. //move calculation to the function itself?
        }
    }
    public class QuantizingOctree
    {
        public QuantOcNode root = null;
        public int colorLimit;
        public QuantOcNode currentNode;

        public int insert(ref QuantOcNode node, Color col, int depth)
        {
            int changeInUniqueColors = 0;

            if (node == null)
            {
                //since the Insert function is recursive, we're already in the branch the color belongs to.

                //CreateAndInit
                node = new QuantOcNode();
                node.depth = depth;
                //
            }
            //else if(node.children==null)
            if (node.isLeaf) //if I only let the last\8 depth nodes be the leaves when adding, it'll be way simpler. Also, memory will still be saved cuz I won't allocate until needed.
                                //Also, if I can also remove in a similar way, it'd be great :'(
            {
               if(node.pixelCount>0)
               {
                    Color nodeColor = node.GetNodeColor();
                    if (nodeColor.B == col.B && nodeColor.R == col.R && nodeColor.G == col.G)
                    {
                        changeInUniqueColors += node.AddPixel(col); //since the Insert function is recursive, we're already in the branch the color belongs to.
                    }
                    else
                    {
                        int nodeBranchIndex = BranchSelector(nodeColor, depth);
                        node.children[nodeBranchIndex] = new QuantOcNode();
                        if (depth < 0 || depth >7)
                            throw new Exception("depth is min 0 to 7 (8 levels) max.");

                        int oldNodePixelCount = node.pixelCount;
                        for (int i=0; i<oldNodePixelCount;i++) //make it more efficient/direct (insteaad of loop, just make way to send sum and pixelcount once)
                        {
                            insert(ref node.children[nodeBranchIndex],nodeColor, depth + 1);
                            node.pixelCount--;
                        }
                        node.red = 0;
                        node.green = 0;
                        node.blue = 0;
                        int branchIndex = BranchSelector(col, depth);
                        insert(ref node.children[branchIndex], col, depth + 1);

                    }
                }
                else
                {
                    changeInUniqueColors += node.AddPixel(col); //since the Insert function is recursive, we're already in the branch the color belongs to.
                }
            }
            else
            {
                int branchIndex = BranchSelector(col, depth);
                changeInUniqueColors += insert(ref node.children[branchIndex],col, depth+1); //need to implement this Branch function (branch selector?)
            }
            foreach(var child in node.children.Where(x=>x!=null))
            {
                child.parent = node;
            }
            return changeInUniqueColors;

        }
        private int BranchSelector(Color col, int depth)
        {
            int branchIndex; // = -1;
            //for(int i=depth;i <= 0;i--)
            //{
                branchIndex =
                (GetBit(col.R, depth) ? 4 : 0)
                | (GetBit(col.G, depth) ? 2 : 0)
                | (GetBit(col.B, depth) ? 1 : 0); //GetBit returns "true" instead of 1 and "false" instead of 0 so we don't need to write GetBit(...)==1 explcitly
            //}
            //if(branchIndex==-1)
            //{
            //    throw new Exception("branchIndex calculation failed.");
            //}
            return branchIndex;
        }
        public int GetUniqueColorCount()
        {
            return root.GetChildTreeColorCount();
        }
        private bool GetBit(byte b, int depthInTree)
        {
            int bitPosition = 7-depthInTree;
            return (b & (1 << bitPosition)) != 0;
        }
    }
}

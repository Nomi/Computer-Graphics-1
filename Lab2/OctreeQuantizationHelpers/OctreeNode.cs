using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;

//Based on: https://github.com/pmichna/NewOctree/

namespace Computer_Graphics_1.Lab2.OctreeQuantizationHelper
{
    internal class OctreeNode
    {
        private static readonly Byte[] Mask = new Byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

        //The root color RGB:
        private int red;
        private int green;
        private int blue;

        //Number of times root count is repeated and its pallete index.
        private int pixelCount;
        private int paletteIndex;

        private readonly OctreeNode[] nodes;

        public OctreeNode(int level, OctreeQuantizer parent) //level - level of parent
        {
            nodes = new OctreeNode[8]; //Noman: Because each Octree node has at most 8 branches (just as the name Octree suggests).

            if (level < 7)
            {
                parent.AddLevelNode(level, this);
            }
        }

        public Boolean IsLeaf //use an actual bool instead??
        {
            get { return pixelCount > 0; } //only leaves are pixels, so only they can have count of pixels (for example black(0,0,0) might be occur in 3 pixels, so pixel count for the leaf for color black will be 3 but the upper levels of the tree (non-leaves) above it will not have count).
        }

        public Color Color
        {
            get
            {
                Color result;

                // determines the color of the leaf
                if (IsLeaf)
                {
                    result = pixelCount == 1 ?
                        Color.FromArgb(255, red, green, blue) :
                        Color.FromArgb(255, red / pixelCount, green / pixelCount, blue / pixelCount);
                    //Here, if the pixelCount is 1: red, green and blue will be the values of that specific pixel. Otherwise, if there are
                    //more than one pixels of a color, the variables red, green, and blue will be the sum of all their values.

                    //Since each addition of the color is the same value (because we're adding same colored pixels as needed for octree/ popularity based color quantization)
                    //and so,  
                    //to get back only the values of color itself, we divide sum of each channel by /pixelCount to get the color.
                }
                else
                {
                    throw new InvalidOperationException("Cannot retrieve a color for other node than leaf.");
                }

                return result;
            }
        }

        public int ActiveNodesPixelCount
        {
            get
            {
                int result = pixelCount;

                // sums up all the pixel presence for all the active nodes
                for (int index = 0; index < 8; index++)
                {
                    OctreeNode node = nodes[index];

                    if (node != null)
                    {
                        result += node.pixelCount;
                    }
                }
                //The above nodes are all leaves as explained near ActiveNodes definition.
                return result;
            }
        }

        public IEnumerable<OctreeNode> ActiveNodes //only colors/leaves (as can be seen from the code structure) [a bit recursive]
        {
            get
            {
                List<OctreeNode> result = new List<OctreeNode>();

                // adds all the active sub-nodes to a list
                for (int index = 0; index < 8; index++)
                {
                    OctreeNode node = nodes[index];

                    if (node != null)
                    {
                        if (node.IsLeaf)
                        {
                            result.Add(node);
                        }
                        else
                        {
                            result.AddRange(node.ActiveNodes); //a bit recursive//
                        }
                    }
                }

                return result;
            }
        }

        public int AddColor(Color color, int level, OctreeQuantizer parent) //level - depth level
        {
            int addedCols = 0;
            // if this node is a leaf, then increase the color amount, and pixel presence
            if (level == 8)
            {
                red += color.R;
                green += color.G;
                blue += color.B;
                pixelCount++;
                if(pixelCount==1)
                {
                    addedCols++;
                }
            }
            else if (level < 8) // otherwise goes one level deeper
            {
                // calculates an index for the next sub-branch
                int index = GetColorIndexAtLevel(color, level); //current level

                // if that branch doesn't exist, grows it
                if (nodes[index] == null)
                {
                    nodes[index] = new OctreeNode(level, parent);
                }

                // adds a color to that branch
                addedCols += nodes[index].AddColor(color, level + 1, parent);
            }
            return addedCols;
            //move quantization here
        }

        public int GetPaletteIndex(Color color, int level)
        {
            int result;

            // if a node is leaf, then we've found the best match already
            if (IsLeaf)
            {
                //add leaf checking
                result = paletteIndex;
            }
            else // otherwise continue in to the lower depths
            {
                int index = GetColorIndexAtLevel(color, level);     //a pinch of recursion and we're done//

                result = nodes[index] != null ? nodes[index].GetPaletteIndex(color, level + 1) : nodes.
                    Where(node => node != null).
                    First().
                    GetPaletteIndex(color, level + 1);
            }

            return result;
        }

        public int RemoveLeaves()
        {
            int result = 0;

            // scans through all the active nodes
            for (int index = 0; index < 8; index++)
            {
                ref OctreeNode node = ref this.nodes[index];

                if (node != null)
                {
                    // sums up their color components
                    this.red += node.red;
                    this.green += node.green;
                    this.blue += node.blue;
                    node.red = 0;
                    node.blue = 0;
                    node.green = 0;
                    // and pixel presence
                    this.pixelCount += node.pixelCount;
                    node.pixelCount = 0;
                    
                    // increases the count of reduced nodes
                    result++;
                }
                
            }

            // returns a number of reduced sub-nodes, minus one because this node becomes a leaf
            return result - 1;
        }

        private static int GetColorIndexAtLevel(Color color, int level) //as explained in lectures.
        {
            return ((color.R & Mask[level]) == Mask[level] ? 4 : 0) |
                   ((color.G & Mask[level]) == Mask[level] ? 2 : 0) |
                   ((color.B & Mask[level]) == Mask[level] ? 1 : 0);
        }

        internal void SetPaletteIndex(int index)
        {
            paletteIndex = index;
        }
    }
}
﻿using Computer_Graphics_1.HelperClasses;
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
    public class OctreeQuantizer
    {

        private OctreeNode root;
        private List<OctreeNode>[] levels;

        public OctreeQuantizer()
        {
            prepare();
        }

        public void Init()
        {
            levels = new List<OctreeNode>[7];
            // creates the octree level lists
            for (Int32 level = 0; level < 7; level++) //we go seven levels deeper from the root (first layer) for an Octree
            {
                levels[level] = new List<OctreeNode>(); //initializes levels
            }
            root = new OctreeNode(0, this);
        }

        internal IEnumerable<OctreeNode> Leaves
        {
            get { return root.ActiveNodes.Where(node => node.IsLeaf); }
        }

        internal void AddLevelNode(int level, OctreeNode octreeNode) //adds node at a certain level
        {
            levels[level].Add(octreeNode);
        }

        public int AddColor(Color color) //list stuff
        {
            //keep tack of leaves count and if it exceeds the given color count then we can reduce it in the 
           return root.AddColor(color, 0, this);
        }
        private void prepare()  //re-initialization of octree level list, and this is used in Clear (this func is a copy of Init, wasn't needed probably)
        {
            levels = new List<OctreeNode>[7];
            // creates the octree level lists
            for (Int32 level = 0; level < 7; level++)
            {
                levels[level] = new List<OctreeNode>();
            }
            root = new OctreeNode(0, this);
        }

        public void Clear() //clears and reinitializes octree
        {
            prepare();
        }

        public List<Color> GetPalette(int colorCount)
        {
            List<Color> result = new List<Color>();
            int leafCount = Leaves.Count();
            int paletteIndex = 0;

            // goes through all the levels starting at the deepest, and goes upto a root level 
            //This specific loop doesn't include the root (because the root element is not inside the levels list).
            for (int level = 6; level >= 0; level--)
            {
                // if level contains any node
                if (levels[level].Count > 0)
                {
                    // orders the level node list by pixel presence (those with least pixels are at the top)
                    IEnumerable<OctreeNode> sortedNodeList = levels[level].OrderBy(node => node.ActiveNodesPixelCount);

                    // removes the nodes unless the count of the leaves is lower or equal than our requested color count
                    foreach (OctreeNode node in sortedNodeList)
                    {
                        // removes a node
                        leafCount -= node.RemoveLeaves();

                        // if the count of leaves is lower then our requested count terminate the loop
                        if (leafCount <= colorCount) break;
                    }

                    // if the count of leaves is lower then our requested count terminate the level loop as well
                    if (leafCount <= colorCount) break;

                    // otherwise clear whole level, as it is not needed anymore
                    levels[level].Clear();
                }
            }

            // goes through all the leaves that are left in the tree (there should now be less or equal than requested)
            foreach (OctreeNode node in Leaves.OrderByDescending(node => node.ActiveNodesPixelCount))
            {
                if (paletteIndex >= colorCount) break;

                // adds the leaf color to a palette
                if (node.IsLeaf)
                {
                    result.Add(node.Color);
                }

                // and marks the node with a palette index
                node.SetPaletteIndex(paletteIndex++);
            }

            // we're unable to reduce the Octree with enough precision, and the leaf count is zero
            if (result.Count == 0)
            {
                throw new NotSupportedException("The Octree contains after the reduction 0 colors.");
            }

            // returns the palette
            return result;
        }

        public int GetPaletteIndex(Color color)
        {
            // retrieves a palette index
            return root.GetPaletteIndex(color, 0);
        }

        public List<Color> LimitPalette(int colorCount)
        {
            List<Color> result = new List<Color>();
            int leafCount = Leaves.Count();
            int paletteIndex = 0;

            // goes through all the levels starting at the deepest, and goes upto a root level 
            //This specific loop doesn't include the root (because the root element is not inside the levels list).
            for (int level = 6; level >= 0; level--)
            {
                // if level contains any node
                if (levels[level].Count > 0)
                {
                    // orders the level node list by pixel presence (those with least pixels are at the top)
                    IEnumerable<OctreeNode> sortedNodeList = levels[level].OrderBy(node => node.ActiveNodesPixelCount);

                    // removes the nodes unless the count of the leaves is lower or equal than our requested color count
                    foreach (OctreeNode node in sortedNodeList)
                    {
                        // removes a node
                        leafCount -= node.RemoveLeaves();

                        // if the count of leaves is lower then our requested count terminate the loop
                        if (leafCount <= colorCount) break;
                    }

                    // if the count of leaves is lower then our requested count terminate the level loop as well
                    if (leafCount <= colorCount) break;

                    // otherwise clear whole level, as it is not needed anymore
                    levels[level].Clear();
                }
            }

            // goes through all the leaves that are left in the tree (there should now be less or equal than requested)
            foreach (OctreeNode node in Leaves.OrderByDescending(node => node.ActiveNodesPixelCount))
            {
                if (paletteIndex >= colorCount) break;

                // adds the leaf color to a palette
                if (node.IsLeaf)
                {
                    result.Add(node.Color);
                }

                // and marks the node with a palette index
                node.SetPaletteIndex(paletteIndex++);
            }
            if(this.root.IsLeaf && result.Count==0)
            {
                result.Add(root.Color);
            }

            // we're unable to reduce the Octree with enough precision, and the leaf count is zero
            if (result.Count == 0)
            {
                throw new NotSupportedException("The Octree contains after the reduction 0 colors.");
            }

            // returns the palette
            return result;
        }
    }
}

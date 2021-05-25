using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ToolLibrary
{
    public class MemberCollection : iMemberCollection
    {
        private Member root;
        private Member[] membersArray;
        private int number;

        public int Number { get => number; set => number = value; }


        public MemberCollection()
        {
            root = null;
            number = 30;
            membersArray = new Member[number];
        }

        public void add(Member aMember)
        {
            if (root == null)
            {
                root = aMember;
            } else
            {
                add(aMember, root);
            }
        }

        private void add(Member aMember, Member ptr)
        {
            int compared = aMember.CompareTo(ptr);
            if (compared == 0) return;
            if (compared < 0)
            {
                if (ptr.LChild == null)
                {
                    ptr.LChild = aMember;
                } else
                {
                    add(aMember, ptr.LChild);
                }
            }
            else
            {
                if (ptr.RChild == null)
                {
                    ptr.RChild = aMember;
                } else
                {
                    add(aMember, ptr.RChild);
                }
            }
            

        }

        // there are three cases to consider:
        // 1. the node to be deleted is a leaf
        // 2. the node to be deleted has only one child 
        // 3. the node to be deleted has both left and right children
        public void delete(Member aMember)
        {
            // search for item and its parent
            Member ptr = root;
            Member parent = null;
            while(ptr != null && aMember.CompareTo(ptr) != 0)
            {
                parent = ptr;
                if (aMember.CompareTo(ptr) < 0)
                {
                    ptr = ptr.LChild;
                }
                else
                {
                    ptr = ptr.RChild;
                }
            }

            // if the search was successful
            if (ptr != null)
            {
                // case 3: item has two children
                if (ptr.LChild != null && ptr.RChild != null)
                {
                    // find the right-most node in left subtree of ptr
                    if (ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
                    {
                        ptr = ptr.LChild;
                        ptr.LChild = ptr.LChild.LChild;
                    }
                    else
                    {
                        Member p = ptr.LChild;
                        Member pp = ptr; // parent of p
                        while (p.RChild != null)
                        {
                            pp = p;
                            p = p.RChild;
                        }
                        // copy the item at p to ptr
                        ptr = p;
                        pp.RChild = p.LChild;
                    }
                }
                else // cases 1 & 2: item has no or only one child
                {
                    Member c;
                    if (ptr.LChild != null)
                    {
                        c = ptr.LChild;
                    }
                    else
                    {
                        c = ptr.RChild;
                    }
                    // remove node ptr
                    if (ptr == root) //need to change root
                    {
                        root = c;
                    }
                    else
                    {
                        if (ptr == parent.LChild)
                        {
                            parent.LChild = c;
                        }
                        else
                        {
                            parent.RChild = c;
                        }
                    }
                }
            }
            Console.WriteLine("Deleted: " + aMember.FirstName + " " + aMember.LastName);
        }

        public bool search(Member aMember)
        {
            return search(aMember, root);
        }

        private bool search(Member aMember, Member root)
        {
            if (root != null)
            {
                if (aMember.CompareTo(root) == 0)
                {
                    return true;

                }
                else
                {
                    if (aMember.CompareTo(root) < 0)
                    {
                        return search(aMember, root.LChild);

                    }
                    else
                    {
                        return search(aMember, root.RChild);

                    }
                }
                                    
            }
                
            return false;
        }

        public Member[] toArray()
        {
            if(membersArray != null)
            {
                Array.Clear(membersArray, 0, membersArray.Length);
            }
            ArrayList list = new ArrayList();
            ArrayList result = inOrderTraverse(root, list);
            for(int i = 0; i < result.Count; i++)
            {
                membersArray[i] = (Member)result[i];
            }

            return membersArray;
        }

        // Traverse the binary tree and create an ArrayList containing elements, for better flexibility with array size.
        private ArrayList inOrderTraverse(Member ptr, ArrayList list)
        {
            if (ptr.LChild != null)
            {
                inOrderTraverse(ptr.LChild, list);
            }

            list.Add(ptr);

            if (ptr.RChild != null)
            {
                inOrderTraverse(ptr.RChild, list);
            }

            return list;
        }
    }
}

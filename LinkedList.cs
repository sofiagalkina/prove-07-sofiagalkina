using System.Collections;

namespace prove_07;

/// <summary>
/// Implements a basic doubly linked list of integers
/// </summary>
public class LinkedList : IEnumerable<int> {
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Adds a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void AddFirst(int value) {
        // Create new node
        Node newNode = new Node(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null) {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

  
    public void AddLast(int value) {
        Node newNode = new Node(value);

        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail;
            _tail.Next = newNode;
            _tail = newNode;
        }
    }


    /// <summary>
    /// Removes the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveFirst() {
        if (_head == _tail) {
            _head = null;
            _tail = null;
        }
        
        else if (_head is not null) {
            _head.Next!.Prev = null; 
            _head = _head.Next; 
        }
    }


    /// <summary>
    /// Removes the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveLast() {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_tail is not null)
        {
            _tail.Prev!.Next = null; 
            _tail = _tail.Prev; 
        }
    }

    /// <summary>
    /// Adds 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void AddAfter(int value, int newValue) {
        Node? curr = _head;
        while (curr is not null) {
            if (curr.Data == value) {
           
                if (curr == _tail) {
                    AddLast(newValue);
                }
                else {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; 
                    newNode.Next = curr.Next; 
                    curr.Next!.Prev = newNode; 
                    curr.Next = newNode; 
                }
                return; 
            }
            curr = curr.Next; // go to the next node
        }
    }

    /// <summary>
    /// Removes the first node that contains 'value'.
    /// </summary>
    public void Remove(int value) {
        Node? current = _head;

        while (current != null)
        {
            if (current.Data == value)
            {
                if (current == _head)
                {
                    RemoveFirst();
                }
                else if (current == _tail)
                {
                    RemoveLast();
                }
                else
                {
                    // skip if node is in the middle
                    current.Prev!.Next = current.Next;
                    current.Next!.Prev = current.Prev;
                }

                return; 
            }

            current = current.Next;
        }
    }

    /// <summary>
    /// Searches for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue) {
        Node? current = _head;

        while (current != null)
        {
            if (current.Data == oldValue)
            {
                current.Data = newValue;
            }

            current = current.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator() {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null) {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse() {
        Stack<int> stack = new Stack<int>();

        // push in normal order
        foreach (var item in this)
        {
            stack.Push(item);
        }

        // pop in reverse order 
        while (stack.Count > 0)
        {
            yield return stack.Pop();
        }
    }

    public override string ToString() {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }
}
// test
public bool Delete(int id)
{
    if (SearchById(id) == null)
        return false;

    root = DeleteRecursive(root, id);
    return true;
}

/// <summary>
/// Recursively finds and removes the node with the given ID.
/// Case 1 (Leaf): Simply remove the node by returning null.
/// Case 2 (One child): Replace the node with its single child.
/// Case 3 (Two children): Replace with in-order successor (smallest in right subtree),
/// then recursively delete the successor from the right subtree.
/// </summary>
private EmployeeNode DeleteRecursive(EmployeeNode node, int id)
{
    // Base case: node not found
    if (node == null)
        return null;

    // Traverse left if target ID is smaller
    if (id < node.Data.Id)
    {
        node.Left = DeleteRecursive(node.Left, id);
    }
    // Traverse right if target ID is larger
    else if (id > node.Data.Id)
    {
        node.Right = DeleteRecursive(node.Right, id);
    }
    // Node found — handle the three deletion cases
    else
    {
        // Case 1: Leaf node (no children) — remove by returning null
        // Case 2a: Only right child — replace with right child
        if (node.Left == null)
            return node.Right;

        // Case 2b: Only left child — replace with left child
        if (node.Right == null)
            return node.Left;

        // Case 3: Two children — find in-order successor (smallest in right subtree)
        EmployeeNode successor = FindMin(node.Right);

        // Replace current node's data with successor's data
        node.Data = successor.Data;

        // Recursively delete the in-order successor from the right subtree
        node.Right = DeleteRecursive(node.Right, successor.Data.Id);
    }

    return node;
}
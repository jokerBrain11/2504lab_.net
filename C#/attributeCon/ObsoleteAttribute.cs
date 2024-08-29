class ObsoleteAttributeExample
{
    [Obsolete("This method is obsolete. Call NewMethod instead.", true)]
    public void OldMethod()
    {
        Console.WriteLine("It is the old method");
    }

    public void NewMethod()
    {
        Console.WriteLine("It is the new method");
    }

    public ObsoleteAttributeExample()
    {
        // 'ObsoleteAttributeExample.OldMethod()' is obsolete: 'This method is obsolete. Call NewMethod instead.'CS0619
        // OldMethod();
        NewMethod();
    }
}
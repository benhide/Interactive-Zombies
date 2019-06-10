/**
 * @file    FirebaseManager.cs
 * @author  Benjamin Williams <bwilliams@lincoln.ac.uk>
 *
 * @license CC 3.0 <https://creativecommons.org/licenses/by-nc-nd/3.0/>
*/
using SimpleFirebaseUnity;

public static class FirebaseManager
{
    public static void Push(FirebaseSettings settings, object obj, System.Action<Firebase, DataSnapshot> callback = null)
    {
        var testData = new FirebaseTestData(settings);
        testData.logger.push(obj, callback);
    }
}

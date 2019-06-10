using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class FirebaseTestData
{
    public FirebaseSettings settings;
    public FirebaseLogger logger;

    public FirebaseTestData(FirebaseSettings settings)
    {
        this.settings = settings;
        logger = new FirebaseLogger(settings);
    }

    public FirebaseTestData(string url, string apiKey)
    {
        var settings = new FirebaseSettings(url, apiKey);
        this.settings = settings;
        logger = new FirebaseLogger(settings);
    }
}

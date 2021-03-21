/*
 * File:        ReleaseSO.cs
 * Author:      Étienne Ménard
 * Description: ScriptableObject for the Change Log.
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New Release", menuName = "Release", order = 1)]
public class ReleaseSO : ScriptableObject
{
    public string version;      // version number of the release, following semantic versioning (X.X.X)
    public new string name;     // name of the release
    public string description;  // description of the release

    public string[] newTracks;  // array of new tracks
    public string[] changes;    // array of changes in the release
    public string[] bugfixes;   // array of bugs that got fixed
}

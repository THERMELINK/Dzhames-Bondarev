using System.Collections;
using UnityEngine;

interface Ipatrol
{
    void HandlePatrol();
    IEnumerator WaitOnPoint(float seconds);
}

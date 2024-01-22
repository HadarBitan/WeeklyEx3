using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class FalseDestroy : MonoBehaviour {
    [Tooltip("Every object tagged with this tag will trigger the Game Over scene")]
    [SerializeField] string triggeringTag;
    [SerializeField] string sceneName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == triggeringTag && enabled) {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            other.transform.position = Vector3.zero;
            SceneManager.LoadScene(sceneName);
        }
    }
}

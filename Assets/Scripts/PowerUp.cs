using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    /*****************************/
    /* these are shown in the inspector in unity */
    /* this lets you have a dropdown in the inspector */
    public enum PowerupType
    {
        TRANSLATE = 0,
        ROTATE,
        SCALE

    };
    [SerializeField]
    public PowerupType PType;

    [SerializeField]
    public float Speed = 10f;

    [SerializeField]
    public Vector3 deltaVector;

    [SerializeField]
    public Vector3 offset; // offset for children object positions
    /*****************************/

    bool isON = false; // is this powerup active?

    /*****************************/
    /* utility functions you may need or want */
    /* utility function to get a list of children of a given game object */
    public List<GameObject> GetChildren(GameObject obj)
    {
        Debug.Log("GetChildren");
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in obj.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    /* utility function to get a list of ALL children of a given game object */
    public List<GameObject> GetAllChildren(GameObject obj)
    {
        List<GameObject> children = GetChildren(obj);
        for (var i = 0; i < children.Count; i++)
        {
            List<GameObject> moreChildren = GetChildren(children[i]);
            for (var j = 0; j < moreChildren.Count; j++)
            {
                children.Add(moreChildren[j]);
            }
        }
        return children;
    }

    /* utility function to get a list of ALL children of a given game object with a particular NAME */
    public List<GameObject> FindChildrenWithName(string nam, GameObject obj)
    {
        List<GameObject> children = GetAllChildren(obj);
        List<GameObject> results = new List<GameObject>();
        for (var i = 0; i < children.Count; i++)
        {
            if (children[i].name == nam)
                results.Add(children[i].gameObject);
        }
        return results;
    }
    /*****************************/

    /* feel free to use/modify/change all of these */
    /* there are more than one way to solve this problem */
    void applyPowerUp(POWERUP p)
    {
        if (p)
        {
            switch (p.PType)
            {
                case PowerupType.TRANSLATE:
                    /* do something here */
                    break;
                case PowerupType.ROTATE:
                    /* do something here */
                    break;
                case PowerupType.SCALE:
                    /* do something here */
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isON)
        {
            // apply powerup to the object this script is attached to
            // you may wish to do this elsewhere
            applyPowerUp(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // player triggered this
            isON = !isON;
            // make a small thumbnail here and add as a child to the player
            // make a clone of ourselves
            var obj = Instantiate(this, /* player transform */ );

            // change the name of the object, you may wish to use something different 
            // to denote the different powerups 
            string childname = "CHILD";
            obj.name = childname;

            // remove all powerup component scripts from the clone 
            // otherwise you will have an infinite loop and it will crash your PC
            Destroy(obj.GetComponent<POWERUP>());

            // how many children are already attached to the player?
            // you may wish to use a specific powerup name to see how many powerups are already applied
            // hint: think of only having 1 type of powerup shown, maybe you need to do something here or before
            int numChildren = FindChildrenWithName(childname,/* player game object */).Count;

            // set the position of the child based on how many already exist
            obj.transform.localPosition = offset * numChildren;

            // set the scale to be small
            obj.transform.localScale /= 5f;

        }
        else
        {
            pother = collision.gameObject.GetComponent<POWERUP>();
            // think about what this is doing here and modify for your own purposes
            if (collision.gameObject.name.Contains("REVERSE"))
            {
                Speed = -Speed;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RelayBehaviour : Imports {

    private GameManager gManager;
    private Animator animator;
    private GameObject laser;
    private SpawnBehaviour spawn;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        gManager.addRelay(gameObject);
        animator = GetComponent<Animator>();
        laser = FindChild("LaserRelay").gameObject;
        //spawn = GameObject.FindGameObjectWithTag("SpawnObject").GetComponent<SpawnBehaviour>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            gManager.enemyFinish(other.gameObject);
        }
    }

    public void finishGame(bool isVictory)
    {
        animator.SetBool("Victory", isVictory);
        animator.SetBool("EndGame", true);

        if(isVictory)
            StartCoroutine(WaitForAnimation(animator));
    }

    private IEnumerator WaitForAnimation(Animator animator)
    {
        do
        {
            yield return null;
        } while (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Victory") || animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Victory0") || animator.IsInTransition(0));

        laser.SetActive(true);
    }
}

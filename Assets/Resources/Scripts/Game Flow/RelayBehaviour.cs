using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RelayBehaviour : Imports {

    private GameManager gManager;
    private Animator animator;
    private GameObject laser;
    private SpawnBehaviour spawn;
    private GameObject exp1;
    private GameObject exp2;
    private GameObject exp3;

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
        spawn = GameObject.FindGameObjectWithTag("SpawnObject").GetComponent<SpawnBehaviour>();
        exp1 = FindChild("Explosion1").gameObject;
        exp2 = FindChild("Explosion2").gameObject;
        exp3 = FindChild("Explosion3").gameObject;
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
        else
        {
            StartCoroutine("explode");
        }
    }

    private IEnumerator WaitForAnimation(Animator animator)
    {
        do
        {
            yield return null;
        } while (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Victory") || animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Victory0") || animator.IsInTransition(0));

        laser.SetActive(true);
    }

    private IEnumerator explode()
    {
        exp1.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        exp2.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        exp3.SetActive(true);

    }
}

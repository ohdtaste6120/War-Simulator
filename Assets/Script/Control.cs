using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask [] layer;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int attack;

    [SerializeField] Slider Gauge;

    private RaycastHit hit;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        maxHealth = currentHealth;
    }

    void Update()
    {

        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Gauge.value = (float)currentHealth / maxHealth;

        // �������� ������ �� (�Ʊ�/�������� �Ÿ� �ĺ�, ������ �ƹ� ���� �����ָ� �ٴڿ��� �߻��)
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit, 2.0f, layer[0]))
        {
            speed = 0.0f;
            animator.SetBool("Attack State", true); // �����Ǿ� �����ϴ� ��� ����

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
            {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    animator.Rebind(); // �ִϸ����� �ʱ�ȭ
                    hit.transform.GetComponent<Control>().currentHealth -= attack;
                }
            }
            
        }

        else if(Physics.Raycast(ray, out hit, 2.0f, layer[1]))
        {
            speed = 0.0f;
            animator.SetBool("Idle State", true); 
        }

        else
        {
            speed = 2.0f;
            animator.SetBool("Attack State", false);
            animator.SetBool("Idle State", false);
        }
    }
}

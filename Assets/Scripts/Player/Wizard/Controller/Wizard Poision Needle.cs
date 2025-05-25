using UnityEngine;

public class WizardPosisionNeedle : MonoBehaviour
{
    [SerializeField] public int levelSkillPoisionNeedle = 1;
    [SerializeField] private float poisionNeedleRange = 5f;
    [SerializeField] public float poisionNeedleDamage = 10f;
    [SerializeField] private float poisionNeedleCoolDown = 5f;
    [SerializeField] private float poisionNeedleManaCost = 10f;
    [SerializeField] private GameObject poisionNeedleEffectPrefab;

    private float lastCastTime = -999f;
    private Thongtin thongtin;

    void Start()
    {
        thongtin = GetComponent<Thongtin>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Check cooldown
            if (Time.time - lastCastTime < poisionNeedleCoolDown)
                return;

            // Check đủ mana
            if (thongtin == null || thongtin.currentMana < poisionNeedleManaCost)
                return;

            // Trừ mana và set cooldown
            thongtin.currentMana -= poisionNeedleManaCost;
            lastCastTime = Time.time;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, poisionNeedleRange);
            var enemyList = new System.Collections.Generic.List<(GameObject, float)>();
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    float dist = Vector2.Distance(transform.position, hit.transform.position);
                    enemyList.Add((hit.gameObject, dist));
                }
            }
            enemyList.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            int count = Mathf.Min(3, enemyList.Count);
            for (int i = 0; i < count; i++)
            {
                EnemyAI enemy = enemyList[i].Item1.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(poisionNeedleDamage + thongtin.attackDamage);
                    if (poisionNeedleEffectPrefab != null)
                        Instantiate(poisionNeedleEffectPrefab, enemy.transform.position, Quaternion.identity);
                }
            }
        }
    }

    

}


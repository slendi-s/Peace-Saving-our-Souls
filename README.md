![logo](https://sun9-48.userapi.com/ZC8ZLZdXfc9JQ5bUQ9cfdBFd3hKU4kBA733UgA/9hIY3S6_2tE.jpg)
# О Проекте
Игра выполнена в PixelArt формате, каждая анимация была прорисована покадрово.
Для изучения был выбран самый популярный жанр "Платформер", т.к. он имеет очень много различного обучающего материала.
## Блок-схема
![diagram](https://sun9-20.userapi.com/QNKexCkuNfU-3dqGQkjBAXEwOBMiz8vxzsKI6A/l9pxt5B4rjI.jpg)
![diagram](https://sun9-57.userapi.com/qU2MpM9EhMDMIrM_TJSjNEI5lL-NLlmwWcBIiA/EbZMLFg76zk.jpg)
![diagram](https://sun9-16.userapi.com/yVgkLrrzS_i2ukyoNuxzwZkO22Vrzr9PYq_swg/2luoUc37WiI.jpg)
## Процесс игры
Главная задача, отбиваться от волн монстров, которые появляются через n количество времени
и чем дольше игра, тем быстрее и больше будут появлятся враги.
Разберем этот момент
```csharp
public void SpawnEnemy()
    {
        //По истечению этого таймера сложность увеличивается
        timerChanged -= Time.deltaTime;
        if (timerChanged <= 0)
        {
            //Восстанавливаем таймер
            timerChanged = beginTimer;
            //С течением времнеи, сложность увеличивается быстрее
            beginTimer -= 0.25f;
            if (beginTimer <= 0)
            {
            //Создаем больше врагов
                beginTimer = 5;
                enemy = Instantiate(enemyCharacter, 
                    new Vector3(Random.RandomRange(-7f, 70f), 1, 0), Quaternion.identity);
                enemy = Instantiate(enemyCharacter, 
                    new Vector3(Random.RandomRange(-7f, 70f), 1, 0), Quaternion.identity);
                return;
            }
            enemy = Instantiate(enemyCharacter, 
                new Vector3(Random.RandomRange(-7f, 70f), 1, 0), Quaternion.identity);
        }
    }
```
# Сущности
В данном проекте сущностью будет являтся главный персонаж и враг.
На каждой сущности есть 3 класса:
```csharp
public class PlayerComponents : MonoBehaviour
{
    public LayerMask whoIam;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Rigidbody2D characterRB;
    public PolygonCollider2D stayCharacter;
    public PolygonCollider2D croutchCharacter;
    public Animator animatorCharacter;
    public Transform attackPoint;
    public LayerMask enemyLayers;
}
```
> **Примечание** Данный класс содержит в себе все необходимые ссылки на свойства и объекты внутри объекта
```csharp
public class PlayerStats : MonoBehaviour
{
    public float speed = 25f;
    public float scaleXCharacter;
    public float checkRadius;
    public float jumpForce=5f;
    public float extraJump;
    public int extraJumpsValue;
    public float countattack;
    public float attackComboCD=2f;
    public float attackRange = 0.5f;
    public int attackDamage = 50;
    public float prepareAttack = 0.25f;
    public int maxHeal = 100;
    public float currentHealth;
    public float enemyMoveDistance=4f;
    public float visionDistance = 8f;
}
```
> **Примечание** Данный класс хранит в себе полную характеристику персонажа
```csharp
public class PlayerStatements : MonoBehaviour
{
    public bool isRight;
    public bool isGrounded;
    public bool isJump;
    public bool isCroutch;
    public bool isAttack;
    public bool isDead = false;
}
```
> **Примечание** Данный класс отвечает за состояние персонажа
Благодаря этим 3 классам, можно свободно настраивать каждого врага, особенно если враги будут разных типов(Ближний бой, Дальний бой, Магические), тогда очень удобно менять им характеристики
## Диаграмма классов
![diagram](https://sun9-18.userapi.com/wVyL95IK1EtCvlneKZ2DiB1zNx5LLFnz0qsUjQ/Cj2v1J3TQv0.jpg)
# Система
В проекте 2 системы: Передвижения и Атаки.
Все системы публичные и статичные, делает глобальный доступ к системе.
Система работает со значениями сущностей, при этом если удалить систему все значения, всех сущностей останется неизменным.
## Система передвижения
Чтобы переместить любого персонажа
```csharp
//Необходимо передать данные о Rigidbody персонажа, направление движения и скорость перемещения
public void Move(Rigidbody2D targetRB, float direction, float speed)
    {
    //Изменяем скорость
        targetRB.velocity = new Vector2(direction*speed,targetRB.velocity.y);
    }
```
> **Примечание** При такой системе, действия выполняют разные сущности
##Система Атаки
```csharp
//Мередаем системе 3 основных класса сущности, где находятся все переменные
public void Attack(PlayerStatements _pStatements, PlayerStats _pStats, PlayerComponents _pComponents)
    {
        //меняем состояние атаки
        _pStatements.isAttack = true;
        //количество комбо ударов
        _pStats.countattack += 1;
        if (_pStats.countattack==1 || _pStats.countattack == 2 || _pStats.countattack == 3)
        {
            //Задаем время, когда можно нанести комбо удар
            _pStats.attackComboCD = 1.5f;
            //Чтобы не писать один и тот же код несколько раз, 
            //изменяем переменную воспроизведение анимации комбо удара
            var attackCombo = string.Format("AttackCombo{0}", _pStats.countattack);
            //Воспроизводим анимацию
            _pComponents.animatorCharacter.SetTrigger(attackCombo);
            //Функция задержки атаки
            PrepareAttack(_pStats);
            //Функция получения и нанесения урона
            AttackUse(_pStats,_pComponents);
        }
    } 
```
## Нанесение и получение урона
```csharp
public void AttackUse(PlayerStats _pStats, PlayerComponents _pComponents)
    {
        //Создаем круг, настраиваем его радиус, потому что через него и будет проходить урон
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_pComponents.attackPoint.position,
            _pStats.attackRange,_pComponents.enemyLayers);
        //Переходим к работе сущности, которая получила урон
        foreach (Collider2D enemy in hitEnemies)
        {
            //Вычитаем здоровье
            enemy.GetComponent<PlayerStats>().currentHealth -= _pStats.attackDamage;
            //Проверяем на смерть
            if (enemy.GetComponent<PlayerStatements>().isDead == false)
                //Если здоровье еще есть, тогда воспроизводим анимацию "боли"
                enemy.GetComponent<PlayerComponents>().animatorCharacter.SetTrigger("Hurt");
            
            if (enemy.GetComponent<PlayerStats>().currentHealth <= 0 )
            {
                //Если здоровье кончилось, тогда засчитываем очки за смерть и воспроизводим анимацию смерти
                GameController.Instance.score += 1;
                enemy.GetComponent<PlayerStatements>().isDead = true;
                enemy.GetComponent<PlayerComponents>().animatorCharacter.SetBool("isDead",true);
                //Coroutine позволяет полностью воспроизвести анимацию смерти после чего удалить объект
                StartCoroutine(Timer(enemy));
            }
        }
    }
```
> **Примечание** Ввиду своей неопытности не смог реализовать получение и нанесение урона без GetComponent, но для данного проекта это не ресурсозатратно

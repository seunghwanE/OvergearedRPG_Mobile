using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public static Language inst;

    public string[] strArray, skillArr, skillContentsArr, fieldArr, missionContents, missionTitle, tips;
    public string baseLanguage;
    public Sprite koreanLogo, englishLogo;
    public SpriteRenderer logo;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            if (inst != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(inst);
        strArray = new string[230];
        fieldArr = new string[49];
        skillArr = new string[30];
        skillContentsArr = new string[30];
        missionTitle = new string[18];
        tips = new string[11];
        LangaugeSet();
    }

    public void LangaugeSet()
    {
        SetLanguage();
        SetSkillLanguae();
        SetField();
        SetMissiontitleSet();
        SetTip();
    }

    public void SetTip()
    {
        if(DataController.inst.languageStr.Equals("Korean"))
        {
            tips[0] = "저장하지 않고 게임을 삭제하면 데이터가 사라집니다";
            tips[1] = "패시브 스킬 중 전사의 혼은 공격횟수를 올려줍니다";
            tips[2] = "펫은 자동공격을 하며, 캐릭터의 공격력에 영향을 받습니다";
            tips[3] = "장비는 제작할수록 확률이 올라갑니다";
            tips[4] = "스킨은 보유효과가 있습니다";
            tips[5] = "리뷰는 쓰셨나요?";
            tips[6] = "랭킹을 통해 자신의 수준을 확인해보세요";
            tips[7] = "매일 접속하고 출석보상을 획득하세요";
            tips[8] = "치명타율과 회피율이 50%가 넘으면 그 이상값은 치명타데미지로 전환됩니다";
            tips[9] = "플라잉미믹을 터치하면 보상을 획득할 수 있습니다";
            tips[10] = "게임은 템빨입니다";
        }
        else
        {
            tips[0] = "If you delete a game without saving it, your data will disappear";
            tips[1] = "The warrior's spirit in the passive skill increases the number of attacks";
            tips[2] = "Pet attacks automatically, influenced by the character's ability to attack";
            tips[3] = "The more equipment we build, the more likely it is";
            tips[4] = "Skins have a retention effect";
            tips[5] = "Did you write a review?";
            tips[6] = "Check your level with the ranking";
            tips[7] = "Access every day and earn attendance rewards";
            tips[8] = "If the critical batting average and the avoidance rate exceed 50%,\nthe above value will be converted to critical damage";
            tips[9] = "You can earn rewards by touching Flying Chest";
            tips[10] = "OverGeared is Most Important";
        }
    }

    public void SetMissiontitleSet()
    {
        if (DataController.inst.languageStr.Equals("Korean"))
        {
            missionTitle[0] = "강자의 길";
            missionTitle[1] = "아임 템빨러";
            missionTitle[2] = "유능한 용병";
            missionTitle[3] = "개발자가 좋아합니다";
            missionTitle[4] = "돈냄새";
            missionTitle[5] = "신의 사도";
            missionTitle[6] = "몬스터킬러";
            missionTitle[7] = "포식자";
            missionTitle[8] = "지갑전사";
            missionTitle[9] = "광고시청";
            missionTitle[10] = "레이드";
            missionTitle[11] = "레벨업";
            missionTitle[12] = "재력가";
            missionTitle[13] = "권력가";
            missionTitle[14] = "겜돌이";
            missionTitle[15] = "강화";
            missionTitle[16] = "사냥꾼";
            missionTitle[17] = "보스";
        }
        else
		{
            missionTitle[0] = "The Way of the Strong";
            missionTitle[1] = "Im Overgeared";
            missionTitle[2] = "A competent mercenary";
            missionTitle[3] = "The developer likes it";
            missionTitle[4] = "The smell of money";
            missionTitle[5] = "The apostle of God";
            missionTitle[6] = "Monster Killer";
            missionTitle[7] = "Predator";
            missionTitle[8] = "Wallet Warrior";
            missionTitle[9] = "Watch Ad";
            missionTitle[10] = "Raid";
            missionTitle[11] = "Level Up";
            missionTitle[12] = "Rich";
            missionTitle[13] = "A man of power";
            missionTitle[14] = "Gamer";
            missionTitle[15] = "Upgrader";
            missionTitle[16] = "Hunter";
            missionTitle[17] = "Boss";
        }
    }
    public string MissionContentsStr(int id, int max)
    {
        string str = string.Empty;
        if (DataController.inst.languageStr.Equals("Korean"))
        {
            if (id.Equals(0))
            { str = string.Format("레벨 {0} 달성", max+1); }
            else if (id.Equals(1))
            { str = string.Format("장비 {0}회 제작", max); }
            else if (id.Equals(2))
            { str = string.Format("미션 {0}회 완료", max); }
            else if (id.Equals(3))
            { str = string.Format("광고 시청 {0}회 하기", max); }
            else if (id.Equals(4))
            { str = string.Format("상자보상 {0}번 획득", max); }
            else if (id.Equals(5))
            { str = string.Format("신전에 기도 {0}번 하기", max); }
            else if (id.Equals(6))
            { str = string.Format("적에게서 스킬 {0}번 강탈", max); }
            else if (id.Equals(7))
            { str = string.Format("적에게서 스탯 {0}회 강탈", max); }
            else if (id.Equals(8))
            { str = string.Format("현질 {0}번 하기", max); }
            else if (id.Equals(9))
            { str = string.Format("광고 시청 {0}번 하기", max); }
            else if (id.Equals(10))
            { str = string.Format("레이드 {0}번 입장", max); }
            else if (id.Equals(11))
            { str = string.Format("레벨업 {0}회 달성", max); }
            else if (id.Equals(12))
            { str = string.Format("골드 {0}번 소비하기", max); }
            else if (id.Equals(13))
            { str = string.Format("루비 {0}번 소비하기", max); }
            else if (id.Equals(14))
            { str = string.Format("플레이시간 {0}분 달성", max); }
            else if (id.Equals(15))
            { str = string.Format("장비 또는 스킬 강화 {0}번 하기", max); }
            else if (id.Equals(16))
            { str = string.Format("메인 적 {0}번 사냥", max); }
            else if (id.Equals(17))
            { str = string.Format("메인 보스 {0}번 사냥", max); }
        }
        else
        {
            if (id.Equals(0))
            { str = string.Format("Level {0} achieved", max + 1); }
            else if (id.Equals(1))
            { str = string.Format("Equipment manufactured {0} times", max); }
            else if (id.Equals(2))
            { str = string.Format("Mission complete {0} times", max); }
            else if (id.Equals(3))
            { str = string.Format("Watch {0} commercials", max); }
            else if (id.Equals(4))
            { str = string.Format("Get box reward {0} times", max); }
            else if (id.Equals(5))
            { str = string.Format("Pray {0} to the temple", max); }
            else if (id.Equals(6))
            { str = string.Format("Steal {0} Skill From Enemy", max); }
            else if (id.Equals(7))
            { str = string.Format("Steal {0} Stat From Enemy", max); }
            else if (id.Equals(8))
            { str = string.Format("Make a purchase {0} times", max); }
            else if (id.Equals(9))
            { str = string.Format("Watch {0} commercials", max); }
            else if (id.Equals(10))
            { str = string.Format("Enter Raid {0}", max); }
            else if (id.Equals(11))
            { str = string.Format("LevelUp {0} times achieved", max); }
            else if (id.Equals(12))
            { str = string.Format("Consume Gold {0}", max); }
            else if (id.Equals(13))
            { str = string.Format("Consume Dia {0}", max); }
            else if (id.Equals(14))
            { str = string.Format("Achieve {0} minutes of play time", max); }
            else if (id.Equals(15))
            { str = string.Format("Reinforce equipment or skills {0} times", max); }
            else if (id.Equals(16))
            { str = string.Format("Montser Hunt {0}", max); }
            else if (id.Equals(17))
            { str = string.Format("Boss Hunt {0}", max); }
        }
        return str;
    }

    //public void SetLogo()
    //{
    //    if (DataController.inst.languageStr.Equals("Korean"))
    //    { logo.sprite = koreanLogo; }
    //    else
    //    { logo.sprite = englishLogo; }
    //}

    public void SetField()
    {
        if (DataController.inst.languageStr.Equals("Korean"))
        {
            fieldArr[0] = "대형 벌";
            fieldArr[1] = "파랑새";
            fieldArr[2] = "검은 양";
            fieldArr[3] = "게";
            fieldArr[4] = "고슴도치";
            fieldArr[5] = "거북이";
            fieldArr[6] = "늑대";
            fieldArr[7] = "거북괴물";
            fieldArr[8] = "리자드맨";
            fieldArr[9] = "고릴라";
            fieldArr[10] = "원시인";
            fieldArr[11] = "병사";
            fieldArr[12] = "도적";
            fieldArr[13] = "전사";
            fieldArr[14] = "암살자";
            fieldArr[15] = "기사";
            fieldArr[16] = "암흑교도";
            fieldArr[17] = "식인족장";
            fieldArr[18] = "정예기사";
            fieldArr[19] = "용병왕";
            fieldArr[20] = "요괴";
            fieldArr[21] = "악마";
            fieldArr[22] = "뱀피르";
            fieldArr[23] = "유령";
            fieldArr[24] = "괴수";
            fieldArr[25] = "붉은괴수";
            fieldArr[26] = "털괴물";
            fieldArr[27] = "뿔괴물";
            fieldArr[28] = "쥐사범";
            fieldArr[29] = "예티";
            fieldArr[30] = "좀비";
            fieldArr[31] = "도끼좀비";
            fieldArr[32] = "미라";
            fieldArr[33] = "오우거";
            fieldArr[34] = "악령";
            fieldArr[35] = "스켈레톤";
            fieldArr[36] = "광대";
            fieldArr[37] = "데스나이트";
            fieldArr[38] = "뱀파이어";
            fieldArr[39] = "드라큘라";
            fieldArr[40] = "망령";
            fieldArr[41] = "밴시";
            fieldArr[42] = "흡혈귀왕";
            fieldArr[43] = "파라오";
            fieldArr[44] = "킹고릴라";
            fieldArr[45] = "기사왕";
            fieldArr[46] = "기갑골렘";
            fieldArr[47] = "아누비스";
            fieldArr[48] = "드래곤";
        }
        else
        {
            fieldArr[0] = "Big Bee";
            fieldArr[1] = "Blue Bird";
            fieldArr[2] = "Black Sheep";
            fieldArr[3] = "Monster Crap";
            fieldArr[4] = "Hedgehog";
            fieldArr[5] = "Turtle";
            fieldArr[6] = "Wolf";
            fieldArr[7] = "Big Turtle";
            fieldArr[8] = "Lizard Man";
            fieldArr[9] = "Gorilla";
            fieldArr[10] = "Primitive man";
            fieldArr[11] = "Soldier";
            fieldArr[12] = "Thief";
            fieldArr[13] = "Warrior";
            fieldArr[14] = "Assasin";
            fieldArr[15] = "Knight";
            fieldArr[16] = "Darkman";
            fieldArr[17] = "Cannibal";
            fieldArr[18] = "Elite Knight";
            fieldArr[19] = "Mercenary King";
            fieldArr[20] = "Monster";
            fieldArr[21] = "Devil";
            fieldArr[22] = "Vampire";
            fieldArr[23] = "Ghose";
            fieldArr[24] = "Beast";
            fieldArr[25] = "Red Beast";
            fieldArr[26] = "Fur Beast";
            fieldArr[27] = "Horn Beast";
            fieldArr[28] = "RatMan";
            fieldArr[29] = "Yeti";
            fieldArr[30] = "Zombi";
            fieldArr[31] = "Axe Zombi";
            fieldArr[32] = "Mummy";
            fieldArr[33] = "Auger";
            fieldArr[34] = "Evil Spirits";
            fieldArr[35] = "Skelleton";
            fieldArr[36] = "Mad Clown";
            fieldArr[37] = "Death Knight";
            fieldArr[38] = "Vampire Baronage";
            fieldArr[39] = "Dracula";
            fieldArr[40] = "Specter";
            fieldArr[41] = "Banshee";
            fieldArr[42] = "Vampire King";
            fieldArr[43] = "Pharaohs";
            fieldArr[44] = "King Gorilla";
            fieldArr[45] = "Knight King";
            fieldArr[46] = "Amored Golem";
            fieldArr[47] = "Anubis";
            fieldArr[48] = "Dragon";
        }
    }
    public void SetSkillLanguae()
    {
        if (DataController.inst.languageStr.Equals("Korean"))
        {
            skillArr[0] = "허초";
            skillArr[1] = "가호";
            skillArr[2] = "화검";
            skillArr[3] = "혈공";
            skillArr[4] = "치유";
            skillArr[5] = "빙결";
            skillArr[6] = "화공";
            skillArr[7] = "암석격";
            skillArr[8] = "강타";
            skillArr[9] = "뇌격";
            skillArr[10] = "전광석화";
            skillArr[11] = "유체화";
            skillArr[12] = "난도";
            skillArr[13] = "부활";
            skillArr[14] = "무신강림";
            skillArr[15] = "착취";
            skillArr[16] = "약점파악";
            skillArr[17] = "폭권";
            skillArr[18] = "흡마공";
            skillArr[19] = "풍차참";

            skillArr[20] = "근력강화";
            skillArr[21] = "무기제련";
            skillArr[22] = "갑옷제련";
            skillArr[23] = "인내심";
            skillArr[24] = "치명";
            skillArr[25] = "은밀기동";
            skillArr[26] = "신속";
            skillArr[27] = "전사의혼";
            skillArr[28] = "거래";
            skillArr[29] = "명상";
        }
        else
        {
            skillArr[0] = "Deception";
            skillArr[1] = "Protection";
            skillArr[2] = "Fire sword";
            skillArr[3] = "Bloody Attack";
            skillArr[4] = "Heal";
            skillArr[5] = "Frozen";
            skillArr[6] = "Arson";
            skillArr[7] = "Rock Attack";
            skillArr[8] = "Smart Blow";
            skillArr[9] = "Electric Blow";
            skillArr[10] = "Flash";
            skillArr[11] = "Fluidization";
            skillArr[12] = "Shredding";
            skillArr[13] = "Revive";
            skillArr[14] = "Possessed of a God";
            skillArr[15] = "Exploitation";
            skillArr[16] = "Identifying Weaknesses";
            skillArr[17] = "Explosive Blow";
            skillArr[18] = "Absorption";
            skillArr[19] = "Typhoon Slash";

            skillArr[20] = "Muscle Upgrade";
            skillArr[21] = "Weapon Upgrade";
            skillArr[22] = "Amor Upgrade";
            skillArr[23] = "Patience";
            skillArr[24] = "Deadly Skill";
            skillArr[25] = "Stealthy Maneuver";
            skillArr[26] = "Quick";
            skillArr[27] = "Warrior's Spirit";
            skillArr[28] = "Deal";
            skillArr[29] = "Meditation";
        }
    }
    public string SetSkillContentsLanguae(int level, int numb)
    {
        string str = string.Empty;
        if (DataController.inst.languageStr.Equals("Korean"))
        {
            if      (numb.Equals(0)) { str = string.Format("10초마다 적의 공격에 카운터를 날려\n공격력의 {0}% 만큼 피해를 준다", 10 * level); }
            else if (numb.Equals(1)) { str = string.Format("피격시 20%확률로 {0}%의\n피해를 무시한다", level * 5); }
            else if (numb.Equals(2)) { str = string.Format("15초마다 30%확률로 {0}%의\n공격력을 가진 불꽃을 소환한다", 75 * level); }
            else if (numb.Equals(3)) { str = string.Format("공격시 20%의 확률로 적의 피를 장악해\n{0}%의 공격력의 피해를 준다", 10 * level); }
            else if (numb.Equals(4)) { str = string.Format("12초마다 30%확률로 체력의 {0}%를\n회복한다", 4 * level); }
            else if (numb.Equals(5)) { str = string.Format("공격시 {0}%의 확률로 얼음을 소환해\n적에게 100%의 공격력을 입힌다", 8 * level); }
            else if (numb.Equals(6)) { str = string.Format("9초마다 30%확률로 적을 불태우며\n{0}%의 피해를 4번 입힌다", 7 * level); }
            else if (numb.Equals(7)) { str = string.Format("공격시 10%의 확률로 {0}% 추가피해를주는\n암석을 소환한다", 20 * level); }
            else if (numb.Equals(8)) { str = string.Format("공격시 {0}%의 확률로 적에게\n100%의 피해를 추가로 입힌다", 10 * level); }
            else if (numb.Equals(9)) { str = string.Format("8초마다 25%확률로 공격력의\n{0}% 피해를 입히는 벼락을 소환한다", 5 * level); }

            else if (numb.Equals(10)) { str = string.Format("10초마다 30%의 확률로 5초간\n공격속도가 {0}% 상승한다", 5 * level); }
            else if (numb.Equals(11)) { str = string.Format("13초마다 30%의 확률로 5초간\n회피율이 {0}% 상승한다", 5 * level); }
            else if (numb.Equals(12)) { str = string.Format("공격시 15%의 확률로 적을\n{0}%의 공격력으로 6번 벤다", 5 * level); }
            else if (numb.Equals(13)) { str = string.Format("사망시 {0}%의 확률로 부활한다\n1번만 발동", 10 * level); }
            else if (numb.Equals(14)) { str = string.Format("11초 마다 {0}%의 확률로\n5초간 무적상태가 된다", 2 * level); }
            else if (numb.Equals(15)) { str = string.Format("공격시 10%의 확률로 적에게서\n체력을 공격력의 {0}% 만큼 빼았는다", 3 * level); }
            else if (numb.Equals(16)) { str = string.Format("14초 마다 17%의 확률로 치명타를 가한다\n 공격력의 {0}%", 100 * (level + 1)); }
            else if (numb.Equals(17)) { str = string.Format("10초마다 폭발의 힘이담긴 주먹으로\n{0}%의 공격력으로 피해를 준다", 70 + (20 * level)); }
            else if (numb.Equals(18)) { str = string.Format("9초마다 10%확률로 적에게\n{0}% 공격력의 결계를 친다", 100 * level); }
            else if (numb.Equals(19)) { str = string.Format("공격시 {0}%의 확률로 100%의 데미지로\n5번 피해를 입히는 태풍을 소환한다", 3 * level); }

            else if (numb.Equals(20)) { str = string.Format("강화된 근력으로 {0}% 만큼\n추가된 공격력으로 공격한다", 10 * level); }
            else if (numb.Equals(21)) { str = string.Format("무기의 날을 갈아 공격력이\n {0} 만큼 추가된다", 50 * level); }
            else if (numb.Equals(22)) { str = string.Format("담금질이 잘된 갑옷으로 체력이\n{0}만큼 추가된다", 100 * level); }
            else if (numb.Equals(23)) { str = string.Format("강한 정신력으로 {0}% 만큼\n 체력이 추가된다", 10 * level); }
            else if (numb.Equals(24)) { str = string.Format("약점을 꿰뚤어 보는 눈으로\n치명타율이 {0}% 추가된다", 2 * level); }
            else if (numb.Equals(25)) { str = string.Format("은밀한 움직임으로 피격시\n회피율이 {0}% 추가된다", 2 * level); }
            else if (numb.Equals(26)) { str = string.Format("빠른몸놀림을 익혀\n움직임이 {0}% 빨라진다", 5 * level); }
            else if (numb.Equals(27)) { str = string.Format("전사의 혼이 기술을 알려줘\n{0}의 횟수만큼 공격한다", level); }
            else if (numb.Equals(28)) { str = string.Format("장사를 이해하여 스테이지 클리어 보상이\n{0}% 많아진다", 5 * level); }
            else if (numb.Equals(29)) { str = string.Format("깨달음을 얻어 경험치 획득량이\n{0}% 증가한다", 5 * level); }
        }
        else
        {
            if (numb.Equals(0)) { str = string.Format("Every 10 seconds, the counter is fired at the\nenemy's attack, dealing {0}% of the damage", 10 * level); }
            else if (numb.Equals(1)) { str = string.Format("20% chance of shooting,\nignoring {0}% damage", level * 5); }
            else if (numb.Equals(2)) { str = string.Format("Every 15 seconds, 30% chance to summon\na flame with {0}% attack power", 75 * level); }
            else if (numb.Equals(3)) { str = string.Format("20% chance of attack,\n{0}% damage to the enemy's blood.", 10 * level); }
            else if (numb.Equals(4)) { str = string.Format("Every 12 seconds, 30% chance to recover\n{0}% of your health", 4 * level); }
            else if (numb.Equals(5)) { str = string.Format("In case of an attack, we'll summon ice with\na {0}% chance to inflict 100% of the attack on the enemy", 8 * level); }
            else if (numb.Equals(6)) { str = string.Format("Every 9 seconds, 30% chance to burn enemies,\ndealing {0}% damage 4 times", 7 * level); }
            else if (numb.Equals(7)) { str = string.Format("Summon a rock with an additional\n{0}% chance of damage in case of attack.", 20 * level); }
            else if (numb.Equals(8)) { str = string.Format("{0}% chance of attack,\nadditional 100% damage to the enemy.", 10 * level); }
            else if (numb.Equals(9)) { str = string.Format("We summon lightning strikes every 8 seconds,\n25% chance to damage {0}% of the attack", 5 * level); }

            else if (numb.Equals(10)) { str = string.Format("Every 10 seconds, 30% chance to increase\nattack speed by {0}% for 5 seconds.", 5 * level); }
            else if (numb.Equals(11)) { str = string.Format("30% chance every 13 seconds,\n{0}% increase in avoidance rate for 5 seconds", 5 * level); }
            else if (numb.Equals(12)) { str = string.Format("15% chance of attack.\n{0}% chance of attack. 6 times", 5 * level); }
            else if (numb.Equals(13)) { str = string.Format("{0}% chance of death. Only one", 10 * level); }
            else if (numb.Equals(14)) { str = string.Format("Every 11 seconds, a {0}% chance of\nbeing invincible for 5 seconds.", 2 * level); }
            else if (numb.Equals(15)) { str = string.Format("10% chance of attacking, taking\n{0}% of the enemy's physical strength", 3 * level); }
            else if (numb.Equals(16)) { str = string.Format("Every 14 seconds, he's got a 17% chance of\na critical hit. {0}% of attack power", 100 * (level + 1)); }
            else if (numb.Equals(17)) { str = string.Format("Every 10 seconds, a fist containing the force of\nan explosion damages {0}% of the attack", 70 + (20 * level)); }
            else if (numb.Equals(18)) { str = string.Format("Every 9 seconds, 10% chance to strike a\n{0}% chance against the enemy", 100 * level); }
            else if (numb.Equals(19)) { str = string.Format("Summon a typhoon that deals damage 5 times with\n100% damage with {0}% chance of attack.", 3 * level); }

            else if (numb.Equals(20)) { str = string.Format("We're attacking with\n{0}% added strength", 10 * level); }
            else if (numb.Equals(21)) { str = string.Format("We'll change the blade of the weapon,\nand we'll add {0} more attacks", 50 * level); }
            else if (numb.Equals(22)) { str = string.Format("A well-kept armor adds\n{0} more HP", 100 * level); }
            else if (numb.Equals(23)) { str = string.Format("Strong mental strength plus {0}% HP", 10 * level); }
            else if (numb.Equals(24)) { str = string.Format("Got a {0}% critical batting average\nadded to look at his weaknesses", 2 * level); }
            else if (numb.Equals(25)) { str = string.Format("A covert move adds {0}%\nto the rate of avoidance in the attack", 2 * level); }
            else if (numb.Equals(26)) { str = string.Format("Learn to move fast and\nmove {0}% faster.", 5 * level); }
            else if (numb.Equals(27)) { str = string.Format("Warrior's Spirit will give you the skills,\nattack you {0} times", level); }
            else if (numb.Equals(28)) { str = string.Format("Understanding the business increases\nstage clear compensation by {0}%.", 5 * level); }
            else if (numb.Equals(29)) { str = string.Format("Realization increases experience\ngain by {0}%", 5 * level); }
        }
        return str;
    }
    public void SetLanguage()
    {
        if (DataController.inst.languageStr.Equals("Korean"))
        {
            strArray[0] = "대장간";
            strArray[1] = "캐릭터";
            strArray[2] = "필드";
            strArray[3] = "메뉴";
            strArray[4] = "전투입장";
            strArray[5] = "입장";
            strArray[6] = "취소";
            strArray[7] = "클리어 보상";
            strArray[8] = "장비";
            strArray[9] = "유물";
            strArray[10] = "제작하기";
            strArray[11] = "뒤로가기";
            strArray[12] = "재료";
            strArray[13] = "제작 확률";
            strArray[14] = "제작 결과";
            strArray[15] = "한 번 더";
            strArray[16] = "열 번 더";
            strArray[17] = "닫기";
            strArray[18] = "종료";
            strArray[19] = "정말 게임을\n종료하시겠습니까?";
            strArray[20] = "장비를 제작할 수 록 확률이 증가합니다";
            strArray[21] = "재화가 부족합니다";
            strArray[22] = "운";
            strArray[23] = "무기";
            strArray[24] = "방패";
            strArray[25] = "투구";
            strArray[26] = "갑옷";
            strArray[27] = "망토";
            strArray[28] = "악세사리";
            strArray[29] = "스탯";
            strArray[30] = "장비";
            strArray[31] = "스킨";
            strArray[32] = "힘";
            strArray[33] = "민";
            strArray[34] = "체";
            strArray[35] = "운";
            strArray[36] = "-적용중인 스킬-";
            strArray[37] = "가격";
            strArray[38] = "착용하기";
            strArray[39] = "구매하기";
            strArray[40] = "스킬설명";
            strArray[41] = "장착";
            strArray[42] = "업데이트 예정";
            strArray[43] = "스탯이 부족합니다";
            strArray[44] = "잔여 스탯";
            strArray[45] = "공격력";
            strArray[46] = "체력";
            strArray[47] = "치명타율";
            strArray[48] = "행운";
            strArray[49] = "해제";
            strArray[50] = "헬퍼";
            strArray[51] = "등";
            strArray[52] = "심장석";
            strArray[53] = "인벤토리가 꽉 찼습니다";
            strArray[54] = "잠금해제 비용";
            strArray[55] = "구매 성공!";
            strArray[56] = "상점";
            strArray[57] = "프로필";
            strArray[58] = "출석";
            strArray[59] = "설정";
            strArray[60] = "배경음";
            strArray[61] = "효과음";
            strArray[62] = "진동";
            strArray[63] = "키패드 사용";
            strArray[64] = "카메라 흔들림";
            strArray[65] = "절전모드 OFF";
            strArray[66] = "받기";
            strArray[67] = "푸쉬메세지";
            strArray[68] = "전투력";
            strArray[69] = "제작 횟수 :";
            strArray[70] = "사냥 횟수 :";
            strArray[71] = "플레이 기간 :";
            strArray[72] = "치명타 데미지";
            strArray[73] = "기부 횟수 :";
            strArray[74] = "회피율";
            strArray[75] = "전투력 보상";
            strArray[76] = "획득완료";
            strArray[77] = "전투력이 부족합니다";
            strArray[78] = "패키지";
            strArray[79] = "신전";
            strArray[80] = "버프";
            strArray[81] = "재화";
            strArray[82] = "광고제거";
            strArray[83] = "공격력 2배";
            strArray[84] = "체력 2배";
            strArray[85] = "치명타 데미지 2배";
            strArray[86] = "전투보상\n골드 2배";
            strArray[87] = "광고시청";
            strArray[88] = "10분 동안\n자동공격";
            strArray[89] = "5분 동안\n치명타율 2배";
            strArray[90] = "10분 동안\n골드획득 2배";
            strArray[91] = "10분 동안\n공격력 2배";
            strArray[92] = "광고보며 기도하기";
            strArray[93] = "기부하고 스탯받기";
            strArray[94] = "기부하기";
            strArray[95] = "비용";
            strArray[96] = "기부를 하시면 랜덤스탯 또는\n능력치를 획득합니다";
            strArray[97] = "공격력";
            strArray[98] = "체력";
            strArray[99] = "치명타율";
            strArray[100] = "회피율";
            strArray[101] = "분해 결과";
            strArray[102] = "전 단계를 모두 클리어해야 합니다";
            strArray[103] = "이미 받으셨어요..";
            strArray[104] = "메인메뉴로\n돌아가시겠습니까?";
            strArray[105] = "메인메뉴로";
            strArray[106] = "획득보상";
            strArray[107] = "레이드";
            strArray[108] = "스킬";
            strArray[109] = "랭킹";
            strArray[110] = "미션";
            strArray[111] = "게임을 종료 후 다시 시작해주세요!";
            strArray[112] = "강화";
            strArray[113] = "강화비용 :";
            strArray[114] = "강화확률 :";
            strArray[115] = "등";
            strArray[116] = "헬퍼";
            strArray[117] = "심장석";
            strArray[118] = "골드";
            strArray[119] = "최대 레벨입니다";
            strArray[120] = "강화성공!!";
            strArray[121] = "강화실패 ㅠㅠ";
            strArray[122] = "장비 해제 후 가능합니다";
            strArray[123] = "재료가 부족합니다";
            strArray[124] = "슬롯 선택";
            strArray[125] = "패시브스킬은 자동적용됩니다";
            strArray[126] = "이미 장착되어 있는 스킬입니다";
            strArray[127] = "보스 도전";
            strArray[128] = "보스도전을 누르면 다시 도전하실 수 있습니다\n강해진 후 다시 도전하세요!";
            strArray[129] = "광고보고\n부활";
            strArray[130] = "확인";
            strArray[131] = "강화재료 :";
            strArray[132] = "기부하고 스킬받기";
            strArray[133] = "기부를 하시면 랜덤하게\n스킬을 1개 획득합니다\n(누르고 있으면 자동구매)";
            strArray[134] = "쉬움";
            strArray[135] = "보통";
            strArray[136] = "하드";
            strArray[137] = "극악";
            strArray[138] = "지옥";
            strArray[139] = "스킨 보유효과";
            strArray[140] = "공격력\n+500";
            strArray[141] = "공격력\n+1,000";
            strArray[142] = "공격력\n+30%";
            strArray[143] = "공격력\n+100%";
            strArray[144] = "광고가 준비되지 않았습니다\n잠시후에 다시 이용해주세요";
            strArray[145] = "메인";
            strArray[146] = "일일";
            strArray[147] = "달성도";
            strArray[148] = "+8 ATT";
            strArray[149] = "+15 HP";
            strArray[150] = "+1% CRI";
            strArray[151] = "+1% Luck";
            strArray[152] = "오늘은 티켓을 모두 사용하셨습니다";
            strArray[153] = "이전 스테이지부터 클리어해주세요";
            strArray[154] = "10마리 사냥후 도전이 가능합니다";
            strArray[155] = "사냥감 선택";
            strArray[156] = "스타터 특가!!";
            strArray[157] = "인벤토리 슬롯 5개 오픈";
            strArray[158] = "정상적으로 오픈시 보석 750개 필요 ()";
            strArray[159] = "오늘 하루 동안 그만보기";
            strArray[160] = "자동분해";
            strArray[161] = "자동분해는 착용한 장비보다 높은 등급의 아이템을\n제외하고 모두 바로 분해합니다";
            strArray[162] = "20레벨부터 가능합니다";
            strArray[163] = "개인정보 이용약관";
            strArray[164] = "위 내용에 동의합니다";
            strArray[165] = "동의";
            strArray[166] = "전체 분해";
            strArray[167] = "장비하지 않은\n모든 아이템을 분해하시겠습니까?";
            strArray[168] = "분해";
            strArray[169] = "저장된 게임을\n불러오시겠습니까?";
            strArray[170] = "불러오기";
            strArray[171] = "로딩중";
            strArray[172] = "레벨, 전투력, 경험치를 확인할 수 있으며,\n클릭하면 레벨에 따른 보상을 획득합니다.";
            strArray[173] = "상대할 몬스터를 선택할 수 있습니다.";
            strArray[174] = "골드 : 기부, 강화, 제작 등에 사용되는 보편적인 재화\n재료 : 제작에 필요한 재화입니다.\n보석 : 특수재화로 효율이 좋으며 다양한 곳에 쓰입니다";
            strArray[175] = "출석 : 하루에 한번 보상이 지급됩니다.\n미션 : 일일미션과 통합미션으로 달성시 보상을 획득합니다.\n랭킹 : 다른 플레이어와 나의 랭킹을 확인합니다.\n설정 : 자신에게 맞게 설정할 수 있습니다.";
            strArray[176] = "장착스킬 : 현재 자신이 장착한 스킬을 확인할 수 있습니다.";
            strArray[177] = "캐릭터 : 스탯, 능력치, 장비, 스킨 등을 조작할 수 있습니다.\n스킬 : 스킬을 강화하거나 장착 또는 분해 할 수 있습니다.\n대장간 : 장비를 제작할 수 있으며, 제작할수록 숙련도가 오릅니다.\n전투 :" +
                " 특수몬스터를 사냥하며, 터치시 공격이 추가됩니다.\n상점 : 다양한 상품이 구비되어 있으며 많은 이용부탁드립니다\n펫 : 자동공격을 하며 캐릭터의 공격력을 공유합니다";
            strArray[178] = "본인의 캐릭터입니다. 터치시 공격을 시작합니다";
            strArray[179] = "선택되지 않았습니다";
            strArray[180] = "저장 성공";
            strArray[181] = "저장 실패";
            strArray[182] = "불러오기 성공";
            strArray[183] = "불러오기 실패";
            strArray[184] = "게임을 다시 시작해주세요";
            strArray[185] = "메인화면으로\n돌아가시겠습니까?";
            strArray[186] = "도망가기";
            strArray[187] = "탭해서 시작하기!";
            strArray[188] = "개발자 전용 쿠폰\n5만 + 5만 + 5만";
            strArray[189] = "쿠폰사용 완료!";
            strArray[190] = "존재하지 않는 쿠폰입니다";
            strArray[191] = "이 쿠폰은 이미 사용됐습니다";
            strArray[192] = "이미 같은 종류의 쿠폰을 사용했습니다";
            strArray[193] = "잘못된 쿠폰입니다";
            strArray[194] = "펫";
            strArray[195] = "소환하기";
            strArray[196] = "소환결과";
            strArray[197] = "해츨링";
            strArray[198] = "켈베로스";
            strArray[199] = "늑대";
            strArray[200] = "전설";
            strArray[201] = "영웅";
            strArray[202] = "일반";
            strArray[203] = "캐릭터 공격력의";
            strArray[204] = "로그인 실패\n구글로그인 부탁드려요";
            strArray[205] = "레이드 티켓은 하루에 5개씩 충전됩니다";
            strArray[206] = "전사의혼";
            strArray[207] = "스탯초기화";
            strArray[208] = "지옥";
            strArray[209] = "중간계";
            strArray[210] = "신계";
            strArray[211] = "신화 장비";
            strArray[212] = "EX급 아이템5개를\n조합하여 무작위 부위의\n신급장비를 제작합니다";
            strArray[213] = "합성하기";
            strArray[214] = "신급장비는 갑옷, 투구, 방패, 무기 4종류 중 무작위로 제작됩니다";
            strArray[215] = "이미 5개를 선택하셨습니다";
            strArray[216] = "5개의 아이템이 필요합니다";
            strArray[217] = "";
            strArray[218] = "";
            strArray[219] = "";
            strArray[220] = "";

        }
        else
        {
            strArray[0] = "Smith";
            strArray[1] = "Player";
            strArray[2] = "Field";
            strArray[3] = "Menu";
            strArray[4] = "Battle";
            strArray[5] = "Enter";
            strArray[6] = "Cancel";
            strArray[7] = "Clear award";
            strArray[8] = "Equip";
            strArray[9] = "Artifact";
            strArray[10] = "Make";
            strArray[11] = "Back";
            strArray[12] = "Material";
            strArray[13] = "Rate";
            strArray[14] = "Result";
            strArray[15] = "One more";
            strArray[16] = "Ten more";
            strArray[17] = "Close";
            strArray[18] = "Finish";
            strArray[19] = "Are you sure you want\nto end the game?";
            strArray[20] = "The more equipment you build, the more likely you are";
            strArray[21] = "Need more money";
            strArray[22] = "Luck";
            strArray[23] = "Weapon";
            strArray[24] = "Shield";
            strArray[25] = "Helmet";
            strArray[26] = "Amor";
            strArray[27] = "Cape";
            strArray[28] = "Acc";
            strArray[29] = "Stat";
            strArray[30] = "Equip";
            strArray[31] = "Skin";
            strArray[32] = "Str";
            strArray[33] = "Dex";
            strArray[34] = "Hea";
            strArray[35] = "Luc";
            strArray[36] = "-Equip Skill-";
            strArray[37] = "Price";
            strArray[38] = "Equip";
            strArray[39] = "Buy";
            strArray[40] = "Skill Description";
            strArray[41] = "Equip";
            strArray[42] = "Update scheduled";
            strArray[43] = "Need more Stat";
            strArray[44] = "Remain Stat";
            strArray[45] = "Att";
            strArray[46] = "Hp";
            strArray[47] = "Cri";
            strArray[48] = "Luck";
            strArray[49] = "Put Off";
            strArray[50] = "Helper";
            strArray[51] = "Back";
            strArray[52] = "Heart Stone";
            strArray[53] = "inventory is full.";
            strArray[54] = "Unlock Costs";
            strArray[55] = "Purchase Successful!";
            strArray[56] = "Store";
            strArray[57] = "Profile";
            strArray[58] = "Attend";
            strArray[59] = "Setting";
            strArray[60] = "BGM";
            strArray[61] = "Effect Sound";
            strArray[62] = "Vibration";
            strArray[63] = "Keypad";
            strArray[64] = "Camera Shake";
            strArray[65] = "SimpleMode OFF";
            strArray[66] = "Get";
            strArray[67] = "Push Messege";
            strArray[68] = "Power";
            strArray[69] = "Smith :";
            strArray[70] = "Hunt :";
            strArray[71] = "Play Time :";
            strArray[72] = "Cri Damage";
            strArray[73] = "Donations :";
            strArray[74] = "Avoid";
            strArray[75] = "Power Compensation";
            strArray[76] = "Get";
            strArray[77] = "Need Power";
            strArray[78] = "Package";
            strArray[79] = "Temple";
            strArray[80] = "Buff";
            strArray[81] = "Money";
            strArray[82] = "Remove AD";
            strArray[83] = "Att x2";
            strArray[84] = "HP x2";
            strArray[85] = "Cri Damage x2";
            strArray[86] = "Battle\nGold x2";
            strArray[87] = "Watch AD";
            strArray[88] = "10 min\nAutomatic attack";
            strArray[89] = "5 min\nCri Damage x2";
            strArray[90] = "10 min\nGold x2";
            strArray[91] = "10 min\nAtt x2";
            strArray[92] = "Praying while watching AD";
            strArray[93] = "Donate and get a Stat";
            strArray[94] = "Donate";
            strArray[95] = "Cost";
            strArray[96] = "If you donate,\nyou'll get one random stat or ability";
            strArray[97] = "Att";
            strArray[98] = "Hp";
            strArray[99] = "Cri";
            strArray[100] = "Avoid";
            strArray[101] = "Delete Result";
            strArray[102] = "Need to clear before step";
            strArray[103] = "Already Get";
            strArray[104] = "Come back Main?";
            strArray[105] = "Go Main";
            strArray[106] = "Get Reward";
            strArray[107] = "Raid";
            strArray[108] = "Skill";
            strArray[109] = "Rank";
            strArray[110] = "Task";
            strArray[111] = "Please end the game and start again!";
            strArray[112] = "Upgrade";
            strArray[113] = "Cost :";
            strArray[114] = "Rate :";
            strArray[115] = "Back";
            strArray[116] = "Helper";
            strArray[117] = "Heart Stone";
            strArray[118] = "Gold";
            strArray[119] = "Max Level!";
            strArray[120] = "Upgrade!!";
            strArray[121] = "Fail ㅠㅠ";
            strArray[122] = "after the equipment is released";
            strArray[123] = "not enough.";
            strArray[124] = "Select Slot";
            strArray[125] = "passive skill is automatically applied";
            strArray[126] = "Already Equip";
            strArray[127] = "Boss Challenge";
            strArray[128] = "If you press [Boss Challenge], you can try again.\nBe strong and try again!";
            strArray[129] = "Watch AD\nRevive";
            strArray[130] = "Ok";
            strArray[131] = "Need :";
            strArray[132] = "Donate and Receive Skill";
            strArray[133] = "If you donate, you will\nearn one skill at random";
            strArray[134] = "easy";
            strArray[135] = "normal";
            strArray[136] = "hard";
            strArray[137] = "nightmare";
            strArray[138] = "hell";
            strArray[139] = "Skin Retention Effect";
            strArray[140] = "Att\n+500";
            strArray[141] = "Att\n+1,000";
            strArray[142] = "Att\n+30%";
            strArray[143] = "Att\n+100%";
            strArray[144] = "The advertisement is not ready\nPlease use it again in a few minutes.";
            strArray[145] = "Main";
            strArray[146] = "Day";
            strArray[147] = "Achievement";
            strArray[148] = "+8 ATT";
            strArray[149] = "+15 HP";
            strArray[150] = "+1% CRI";
            strArray[151] = "+1% Luck";
            strArray[152] = "You have used all your tickets today";
            strArray[153] = "Please clear the previous stage";
            strArray[154] = "You can try 10 after hunting";
            strArray[155] = "Choice Monster";
            strArray[156] = "Start Event!!";
            strArray[157] = "Inventory slot 5ea Open!";
            strArray[158] = "750 gems required for normal opening";
            strArray[159] = "Let's stop seeing me today";
            strArray[160] = "Auto\nDelete";
            strArray[161] = "Automatic deleting immediately breaks down items\nthat are lower than the equipment worn.";
            strArray[162] = "Lv.20";
            strArray[163] = "Terms and Conditions of Personal Information";
            strArray[164] = "I agree with the above";
            strArray[165] = "Agree";
            strArray[166] = "Total Delete";
            strArray[167] = "Are you sure you want to disassemble\nall items that are not equipped?";
            strArray[168] = "Delete";
            strArray[169] = "Would you like to\nLoad a saved game?";
            strArray[170] = "Load";
            strArray[171] = "Loading";
            strArray[172] = "You can check your level, combat power, and experience,\nand click to earn rewards based on your level.";
            strArray[173] = "You can select which monsters to face";
            strArray[174] = "Gold: Universal goods used for donation, reinforcement, and production\nMaterial: Goods needed for production.\nDia: Special goods are efficient and used in various places";
            strArray[175] = "Attendance: You will be rewarded once a day.\nMission: Earn rewards when achieved with daily and integrated missions.\nRanking : Check my ranking with other players.\nSettings: You can set them to suit yourself";
            strArray[176] = "Mounting Skills: You can see which skills you currently have";
            strArray[177] = "Player: You can operate the stat, capability, equipment, skin, etc.\nSkill : Allows you to strengthen, mount, or disassemble the skill.\nSmiths: You can manufacture equipment, and the more you produce it, the more skilled you become.\nRaid: hunts special monsters, and attacks are added on touch.\nShop: Various products are available and please use them\nPet: Auto-attack and share character's attack power";
            strArray[178] = "It's your character. Touch to launch an attack.";
            strArray[179] = "No selection made no selection";
            strArray[180] = "Save Success";
            strArray[181] = "Save Fail";
            strArray[182] = "Load Success";
            strArray[183] = "Load Fail";
            strArray[184] = "Please restart the game";
            strArray[185] = "Are you sure you want to\nreturn to the main screen?";
            strArray[186] = "Run Away";
            strArray[187] = "Tap to Start!";
            strArray[188] = "";
            strArray[189] = "Complete Using Coupon!";
            strArray[190] = "This coupon does not exist";
            strArray[191] = "This coupon has already been used";
            strArray[192] = "You've already used the same type of coupon";
            strArray[193] = "Invalid coupon";
            strArray[194] = "Pet";
            strArray[195] = "Summoning";
            strArray[196] = "Result";
            strArray[197] = "Hatchling";
            strArray[198] = "Cerberus";
            strArray[199] = "Wolf";
            strArray[200] = "Lengend";
            strArray[201] = "Epic";
            strArray[202] = "Normal";
            strArray[203] = "character attack";
            strArray[204] = "Login Fail\nplease google login";
            strArray[205] = "Raid tickets are charged five times a day";
            strArray[206] = "Warrior's Spirit";
            strArray[207] = "Reset Stat";
            strArray[208] = "Hell";
            strArray[209] = "Earth";
            strArray[210] = "God";
            strArray[212] = "God Item";
            strArray[212] = "";
            strArray[213] = "";
            strArray[214] = "";
            strArray[215] = "";
            strArray[216] = "";
            strArray[217] = "";
            strArray[218] = "";
            strArray[219] = "";
            strArray[220] = "";
        }
    }
}


using System.Collections.Generic;
using UnityEngine;
class RoleContainer {

    private const string protectionIcon = "Icons/RoleProtection";
    private const string damageIcon = "Icons/RoleDamage";
    private const string supportIcon = "Icons/RoleSupport";

    private static Dictionary<RoleEnum, Sprite> roleSprites;

    public static void Initialize() {
        roleSprites = new Dictionary<RoleEnum, Sprite>();
        roleSprites.Add(RoleEnum.PROTECTION, Resources.Load<Sprite>(protectionIcon));
        roleSprites.Add(RoleEnum.DAMAGE, Resources.Load<Sprite>(damageIcon));
        roleSprites.Add(RoleEnum.SUPPORT, Resources.Load<Sprite>(supportIcon));
    }

    public static Sprite GetIconForRole(RoleEnum role) {
        if (roleSprites == null) {
            Initialize();
        }
        return roleSprites[role];
    }
}

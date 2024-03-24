# 3rdPersonController-Soldier-Prototype-
3rd Person kontrollerine sahip bir shooter karakteri için geliştirilmiş olan prototipi içeren repo'dur.<br>
Çok fazla if condition olacağından, basit bir State Machine kullanılmıştır. <br>
Bu prototipte Player => Yürüme, Koşma, Eğilme, Zıplama, Nişan alma, Silah değiştirme, Silah Reload etme gibi özelliklere sahiptir. Basit bir recoil sistemi, ses yapısı eklenmiştir. Basit bir düşman ve düşman hasar alma/ölme(Ragdoll ile) mekaniği mevcuttur.<br><br>

Movement animations için Blend Tree kullanılmıştır. Weapon animations için Animation Layers ve Mask kullanılmıştır.<br><br>

---MOVEMENT SINIFLARI---
- MovementStateManager.cs => Tüm temel hareket logic'i buradadır. Hareket datası da buradadır. İlerde data kısımlarını Scriptable Object'e taşıyarak designer friendly hale getirebiliriz. Character Controller üzerinden hareketi sağlıyoruz. <br>
  GetDirectionAndMove(); =>
  HandleGravity();
  IsFalling();



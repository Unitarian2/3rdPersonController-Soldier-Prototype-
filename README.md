# 3rdPersonController-Soldier-Prototype-
3rd Person kontrollerine sahip bir shooter karakteri için geliştirilmiş olan prototipi içeren repo'dur.<br>
Çok fazla if condition olacağından, basit bir State Machine kullanılmıştır. <br>
Bu prototipte Player => Yürüme, Koşma, Eğilme, Zıplama, Nişan alma, Silah değiştirme, Silah Reload etme gibi özelliklere sahiptir. Basit bir recoil sistemi, ses yapısı eklenmiştir. Basit bir düşman ve düşman hasar alma/ölme(Ragdoll ile) mekaniği mevcuttur.<br><br>
Yapılabilecek geliştirmeler => New input system geçilebilir. Bunun benzer bir örneğini görmek için : https://github.com/Unitarian2/MediatorPatternExample-FruitTreeGrowing-Prototype-with-Generic-Type-Finite-State-Machine<br><br>

Movement animations için Blend Tree kullanılmıştır. Weapon animations için Animation Layers ve Mask kullanılmıştır.<br><br>

---MOVEMENT SINIFLARI---
Update metodu içinde en tüm movement metodlarını çalıştırdıktan sonra en son Animator.SetFloat ile BlendTree'ye veri beslemesi yapıyoruz. Bu sayede movement animation'lar değişiyor.
- MovementStateManager.cs => Tüm temel hareket logic'i buradadır. Hareket datası da buradadır. İlerde data kısımlarını Scriptable Object'e taşıyarak designer friendly hale getirebiliriz. Character Controller üzerinden hareketi sağlıyoruz.<br>
  GetDirectionAndMove()=> Bu prototipte old input system kullandık. X ve Y direction'ları alıp, karakterin yerde veya havada olmasına göre Move metoduyla karakteri her frame hareket ettiriyoruz.
  HandleGravity() => Yerçekimi hareketini veriyoruz. Karakter zaten yerdeyse gravity biraz azalttık ama 0 yapmıyoruz çünkü "floating" duruma düşmek istemiyoruz.
  IsFalling() => Bu sadece karakter düşüş durumundaysa animator güncellemek için kullandığımız bir metod.
- CrouchState.cs => CrouchState'deyken, Left Shift yani koşma tuşuna basarsak Crouch iptal edip RunState geçiyoruz. Eğilme tuşuna(C) tekrar basarsak karakterin hareket miktarına göre Idle veya Walk state'e geçiyoruz. movementManager.inputZ kontrol ederek de ileri geri eğilme hızını güncelliyoruz.(Geriye daha yavaş hareket eder.)
- IdleState.cs => Karakterin direction.magnitude değerine göre Walk veya Run State'e geçiyoruz. Eğilme tuşuna basıldıysa(C) Crouch State geçiyoruz. Zıplama tuşuna(Space) basıldıysa



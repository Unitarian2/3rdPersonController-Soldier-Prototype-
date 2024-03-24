# 3rdPersonController-Soldier-Prototype-
3rd Person kontrollerine sahip bir shooter karakteri için geliştirilmiş olan prototipi içeren repo'dur.<br>
Çok fazla if condition olacağından, basit bir State Machine kullanılmıştır. <br>
Bu prototipte Player => Yürüme, Koşma, Eğilme, Zıplama, Nişan alma, Silah değiştirme, Silah Reload etme gibi özelliklere sahiptir. Basit bir recoil sistemi, ses yapısı eklenmiştir. Basit bir düşman ve düşman hasar alma/ölme(Ragdoll ile) mekaniği mevcuttur. Unity'nin animation rigging'ini kullandığımız için prosedürel olarak karakter rig'inin belli bölgeleri hareket etmekte.<br><br>
Yapılabilecek geliştirmeler => New input system geçilebilir. Bunun benzer bir örneğini görmek için : https://github.com/Unitarian2/MediatorPatternExample-FruitTreeGrowing-Prototype-with-Generic-Type-Finite-State-Machine<br><br>

Movement animations için Blend Tree kullanılmıştır. Weapon animations için Animation Layers ve Mask kullanılmıştır.<br><br>

---<b>MOVEMENT SINIFLARI</b>---<br>
Update metodu içinde en tüm movement metodlarını çalıştırdıktan sonra en son Animator.SetFloat ile BlendTree'ye veri beslemesi yapıyoruz. Bu sayede movement animation'lar değişiyor.<br>
- <b>MovementStateManager.cs</b> => Tüm temel hareket logic'i buradadır. Hareket datası da buradadır. İlerde data kısımlarını Scriptable Object'e taşıyarak designer friendly hale getirebiliriz. Character Controller üzerinden hareketi sağlıyoruz.<br>
  GetDirectionAndMove()=> Bu prototipte old input system kullandık. X ve Y direction'ları alıp, karakterin yerde veya havada olmasına göre Move metoduyla karakteri her frame hareket ettiriyoruz.<br>
  HandleGravity() => Yerçekimi hareketini veriyoruz. Karakter zaten yerdeyse gravity biraz azalttık ama 0 yapmıyoruz çünkü "floating" duruma düşmek istemiyoruz.<br>
  IsFalling() => Bu sadece karakter düşüş durumundaysa animator güncellemek için kullandığımız bir metod.<br>
- <b>CrouchState.cs</b> => CrouchState'deyken, Left Shift yani koşma tuşuna basarsak Crouch iptal edip RunState geçiyoruz. Eğilme tuşuna(C) tekrar basarsak karakterin hareket miktarına göre Idle veya Walk state'e geçiyoruz. movementManager.inputZ kontrol ederek de ileri geri eğilme hızını güncelliyoruz.(Geriye daha yavaş hareket eder.)<br>
- <b>IdleState.cs</b> => Karakterin direction.magnitude değerine göre Walk veya Run State'e geçiyoruz. Eğilme tuşuna basıldıysa(C) Crouch State geçiyoruz. Zıplama tuşuna(Space) basıldıysa Jump State geçiyoruz.<br>
- <b>JumpState.cs</b> => Jump State'e hangi state'den geçtiğimiz önemlidir. Buna göre iki farklı Jump animation çalıştırıyoruz. Jump bittiğinde hareket durumu ve basılan tuşlara göre Run, Walk veya Idle State'e geçiş yapıyoruz.<br>
- <b>RunState.cs</b> => LeftShift tuşuna basmayı bırakırsak(GetKeyUp) Walk State geçiyoruz. Karakter hareketi durduysa Idle State geçiyoruz. inputZ'ye göre ileri ve geri koşma hızı ayarlanıyor. Zıplama tuşuna(Space) basıldığındaysa Jump State geçiyoruz.<br>
- <b>WalkState.cs</b> => LeftShift basıldıysa Run State geçiyoruz. C tuşuna basıldıysa Crouch state geçiyoruz. Hareket durduysa Idle State geçiyoruz. inputZ'ye göre ileri ve geri yürüme hızı ayarlanıyor. Zıplama tuşuna(Space) basıldığındaysa Jump State geçiyoruz.<br><br>

---<b>AIM SINIFLARI</b>---<br>
Aim işlemi için old input systemdan gelen harekete göre karakteri döndürüyoruz. Virtual Camera Player object'in bir child object'ine(RecoilFollowPos) follow ve look yaparak takip etmekte.<br>

- <b>AimStateManager.cs</b> => Ateş ettiğinde mermiyi göndereceğimiz yeri tespit için aim position hesaplaması yapılır(actualAimPos). LeftAlt tuşu ile kamerayı sağ omuz veya sol omuz şeklinde konumlandırabiliyoruz(CameraTransition). LateUpdate'de karakter ve kamera aim hareketleri gerçekleşiyor.<br>
- <b>HipfireState.cs</b> => Aimin'deki Idle state olarak düşünülebilir. Sağ mouse tuşuna basılınca Aim State'e geçiyor.<br>
- <b>AimState.cs</b> => Nişan alma state'i. Fov azalır. Sağ mouse tuşuna bırakılınca Hipfire State'e geçiyor.<br><br>

---<b>ACTION SINIFLARI</b>---<br>
<b>ActionStateManager.cs</b> => Ses çalma, silah değiştirme, mermi reload gibi işlemleri tetikleyen metodlara sahiptir. Animaton event'ler ile bu sınıfları ilgili zamanda çağırıyoruz.<br>
<b>ActionDefaultState.cs</b> => Idle State'dir. Idle state'de Character Silahı tutar. Duruma göre Reload veya Weapon Swap state'lerine geçiş sağlar.<br>
<b>ActionReloadState.cs</b> => Animation Rigging ile silaha bağladığımız eller serbest kalır. Ardından reload animasyonu başlar.<br>
<b>ActionWeaponSwapState.cs</b> => Animation Rigging ile silaha bağladığımız eller serbest kalır. Ardından Weapon swap animasyonu başlar.<br><br>

---<b>WEAPON SINIFLARI</b>---<br>
<b>Bullet.cs</b> => Bu sınıf bir mermi object'i temsil eder. EnemyHealth'e sahip bir object'e temas ederse hasar verir veya öldürür.<br>
<b>WeaponAmmo.cs</b> => Silah reload'undan sorumludur. Reload metodu çağırılınca, şarjördeki mermi sayısı ve yedekteki mermi sayısına göre şarjörü doldurur.<br>
<b>WeaponBloom.cs</b> => Bu sınıf silah ateş ettiğinde, barrel üzerinde bir parlama efekti oluşturacak. Bu sınıftan o efekt'in açısını elde ediyoruz. Efekt'in açısı karakterin hangi state'de olduğuna göre değişkenlik gösterir.<br>
<b>WeaponClassManager.cs</b> => Bunu weapon inventory gibi düşünebiliriz. Silahlar arasında değişimin yapıldığı sınıftır. Mevcut elde olan silahın index'i burada tutulur. Eldeki ve cepteki silahların da bir listesi bu sınıftadır(weapons). SetCurrentWeapon metodunda sol el IK'sını günceller çünkü her silahta sol elin konumu genelde değişiyor. ChangeWeapon ile bir önceki veya bir sonraki silaha swap yapılır. WeaponPutAway metodu animation event ile tetiklenir(Put away animation bitince). Yeni silah çekilince ActionStateManager'da ActionDefaultState'e geçilir(WeaponPulledOut).<br>
<b>WeaponManager.cs</b> => Eldeki silahın ateş edilmesi yönetimi burada sağlanır. Ateş etme(Fire) işlemi burada gerçekleştirilir.<br>
<b>WeaponRecoil.cs</b> => Bu metod ile silah her ateş edildiğinde bir recoil efekti sağlanması için kamerayı geri itiyoruz. TriggerRecoil metodu kamerayı geriye iterken, Update metodu ile her frame akıcı bir şekilde kamera olması gereken konuma yaklaşır.<br><br>

---<b>ENEMY SINIFLARI---<br>
Bu prototipinin amacı Character Controller olduğu için enemy implementasyonu çok temel ve basit tutulmuştur. <br>
<b>EnemyHealth.cs</b> => Düşmanın health'ini tutan sınıftır. EnemyDeath metodu çağırılınca Ragdoll tetikleyerek ölüm hareketi sağlanır. Bullet çarpınca TakeDamage metodunu çağırarak enemy'nin canını azaltır.<br>
<b>RagdollManager.cs</b> => isKinematic değeri güncelleyerek Ragdoll'un çalışmasını sağlar.<br><br>



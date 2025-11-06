using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace NomadMod.Core
{
    /// <summary>
    /// Ana göçebe oba sınıfı - Oyuncunun kurduğu kampı temsil eder
    /// </summary>
    public class Oba
    {
        // Temel Özellikler
        [SaveableProperty(1)]
        public string Id { get; set; }
        
        [SaveableProperty(2)]
        public string ObaAdi { get; set; }
        
        [SaveableProperty(3)]
        public Vec2 Konum { get; set; }
        
        [SaveableProperty(4)]
        public CampaignTime KurulusTarihi { get; set; }
        
        [SaveableProperty(5)]
        public bool Aktif { get; set; }
        
        [SaveableProperty(6)]
        public int Seviye { get; set; }
        
        // Sprint 2: Beyler
        [SaveableProperty(7)]
        public ObaBey TarimBeyi { get; set; }
        
        [SaveableProperty(8)]
        public ObaBey SavasBeyi { get; set; }
        
        [SaveableProperty(9)]
        public ObaBey DiplomatiBeyi { get; set; }
        
        // Sprint 3: Erzak
        [SaveableProperty(10)]
        public ObaErzak Erzak { get; set; }
        
        // Görsel (kaydedilmez, yeniden oluşturulur)
        public Settlement GorselSettlement { get; set; }
        public MobileParty GorselParty { get; set; }
        
        /// <summary>
        /// Serialization için boş constructor
        /// </summary>
        public Oba()
        {
        }
        
        /// <summary>
        /// Yeni oba oluşturur
        /// </summary>
        public Oba(Vec2 konum, string oyuncuAdi)
        {
            Id = Guid.NewGuid().ToString();
            ObaAdi = $"{oyuncuAdi}'nın Obası";
            Konum = konum;
            KurulusTarihi = CampaignTime.Now;
            Aktif = true;
            Seviye = 1;
            
            // Erzak deposu başlat
            Erzak = new ObaErzak();
            
            Console.WriteLine($"[GöçebeSistemi] Yeni oba: {ObaAdi} @ ({konum.X:F1}, {konum.Y:F1})");
        }
        
        /// <summary>
        /// Oba geçerli mi kontrol eder
        /// </summary>
        public bool GecerliMi()
        {
            if (!Aktif) return false;
            if (Konum.X < 0 || Konum.Y < 0) return false;
            return true;
        }
        
        /// <summary>
        /// Oyuncuya mesafe
        /// </summary>
        public float OyuncuyaMesafe()
        {
            try
            {
                if (MobileParty.MainParty?.Position2D != null)
                    return Konum.Distance(MobileParty.MainParty.Position2D);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GöçebeSistemi] Mesafe hatası: {ex.Message}");
            }
            return float.MaxValue;
        }
        
        /// <summary>
        /// Obayı deaktive eder
        /// </summary>
        public void Deaktive()
        {
            Aktif = false;
            Console.WriteLine($"[GöçebeSistemi] Oba deaktive: {ObaAdi}");
        }
    }
}
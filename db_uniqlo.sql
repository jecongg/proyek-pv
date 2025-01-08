/*
SQLyog Community v13.3.0 (64 bit)
MySQL - 10.4.32-MariaDB : Database - db_uniqlo
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`db_uniqlo` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `db_uniqlo`;

/*Table structure for table `barang` */

DROP TABLE IF EXISTS `barang`;

CREATE TABLE `barang` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nama` varchar(255) DEFAULT NULL,
  `harga` int(11) DEFAULT NULL,
  `diskon` int(11) DEFAULT NULL,
  `url_gambar` text DEFAULT NULL,
  `stok_nosize` int(1) DEFAULT NULL COMMENT 'jika -1 artinya ada sizenya',
  `id_kategori` int(11) DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  `deskripsi` text DEFAULT NULL,
  `diskon_start` timestamp NULL DEFAULT NULL,
  `diskon_end` timestamp NULL DEFAULT NULL,
  `returable` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `foreign_barang_kategori` (`id_kategori`),
  CONSTRAINT `foreign_barang_kategori` FOREIGN KEY (`id_kategori`) REFERENCES `kategori` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `barang` */

insert  into `barang`(`id`,`nama`,`harga`,`diskon`,`url_gambar`,`stok_nosize`,`id_kategori`,`deleted_at`,`deskripsi`,`diskon_start`,`diskon_end`,`returable`) values 
(3,'AIRism Katun T-Shirt Oversize | Garis Lengan 1/2',249000,50000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/461914/sub/goods_461914_sub14_3x4.jpg?width=369',-1,1,NULL,'T-shirt oversized dengan desain garis lengan 1/2, nyaman digunakan untuk gaya santai sehari-hari.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(4,'AIRism Cotton Oversized T-Shirt | Striped Half Sleeve',249000,59000,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/461914/item/idgoods_69_461914_3x4.jpg?width=369',-1,1,NULL,'T-shirt katun oversized dengan motif garis, lengan pendek, dan desain modern yang stylish.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(5,'DRY-EX T-Shirt',249000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/465191/item/goods_08_465191_3x4.jpg?width=369',-1,1,NULL,'T-shirt berbahan DRY-EX yang ringan dan cepat kering, cocok untuk aktivitas olahraga.',NULL,NULL,0),
(6,'Dry Color Crew Neck T-Shirt | Short Sleeve',99000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/465187/item/idgoods_55_465187_3x4.jpg?width=369',-1,1,NULL,'Crew neck T-shirt sederhana dengan bahan katun yang lembut dan nyaman.',NULL,NULL,1),
(7,'Mickey Stands Short Sleeve UT',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/477568/sub/goods_477568_sub14_3x4.jpg?width=369',-1,1,NULL,'T-shirt kolaborasi dengan Mickey Mouse, menampilkan desain klasik karakter Disney.',NULL,NULL,1),
(8,'Mickey Stands Short Sleeve UT',199000,70000,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/474199/sub/idgoods_474199_sub1_3x4.jpg?width=369',-1,3,NULL,'T-shirt kolaborasi dengan Mickey Mouse, menampilkan desain unik dengan warna berbeda.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(9,'Sanrio Characters Short Sleeve UT',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/477942/sub/goods_477942_sub14_3x4.jpg?width=369',-1,3,NULL,'T-shirt bergambar karakter Sanrio, ideal untuk para penggemar Sanrio.',NULL,NULL,1),
(10,'Sesame Street Short Sleeve UT',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/474422/sub/goods_474422_sub14_3x4.jpg?width=369',-1,1,NULL,'T-shirt dengan tema Sesame Street, cocok untuk gaya kasual dan penggemar karakter klasik.',NULL,NULL,1),
(11,'Sesame Street Short Sleeve UT',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/478101/sub/goods_478101_sub14_3x4.jpg?width=369',-1,3,NULL,'T-shirt Sesame Street lainnya dengan sentuhan desain yang berbeda.',NULL,NULL,0),
(12,'Ukiyo-E Short Sleeve UT',199000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/477298/sub/idgoods_477298_sub1_3x4.jpg?width=369',-1,3,NULL,'T-shirt Ukiyo-E dengan cetakan khas seni tradisional Jepang.',NULL,NULL,1),
(13,'Ukiyo-E Short Sleeve UT',199000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/477295/sub/idgoods_477295_sub7_3x4.jpg?width=369',-1,1,NULL,'T-shirt Ukiyo-E lainnya, menampilkan seni Jepang yang menarik dan unik.',NULL,NULL,1),
(14,'Zip Up Blouson',699000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/475681/item/idgoods_08_475681_3x4.jpg?width=369',-1,5,NULL,'Blouson dengan resleting depan, memberikan gaya casual dan praktis untuk musim dingin.',NULL,NULL,1),
(15,'KAWS + Warhol Coach Jacket',699000,200000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/476316/sub/goods_476316_sub14_3x4.jpg?width=369',-1,5,NULL,'Jaket Coach kolaborasi KAWS + Warhol, dengan desain artistik dan stylish.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(16,'Hoodie SpongeBob Sweat Hoodie',699000,200000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/476551/sub/goods_476551_sub14_3x4.jpg?width=369',-1,5,NULL,'Sweat hoodie kolaborasi SpongeBob, menampilkan desain karakter ikonik.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(17,'Windproof Stand Blouson (Water-Repellent)',899000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/464918/sub/goods_464918_sub13_3x4.jpg?width=369',-1,5,NULL,'Blouson tahan angin dan tahan air, cocok untuk musim dingin atau cuaca ekstrem.',NULL,NULL,1),
(18,'Seamless Down Parka',1990000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/470077/item/idgoods_09_470077_3x4.jpg?width=369',-1,5,NULL,'Parka tanpa jahitan dengan fitur down, nyaman dan hangat untuk musim dingin.',NULL,NULL,1),
(19,'Sweat Full-Zip Hoodie',599000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/475380/sub/goods_475380_sub14_3x4.jpg?width=369',-1,5,NULL,'Hoodie full-zip dengan desain minimalis, cocok untuk gaya santai sehari-hari.',NULL,NULL,1),
(20,'AIRism Cotton Pique Polo Shirt',299000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/475367/sub/idgoods_475367_sub7_3x4.jpg?width=369',-1,6,NULL,'Polo shirt dengan bahan katun AIRism yang nyaman dan bernapas, sempurna untuk cuaca panas.',NULL,NULL,1),
(21,'Dry Pique Striped Polo Shirt',299000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/472592/sub/goods_472592_sub14_3x4.jpg?width=369',-1,6,NULL,'Polo shirt dengan motif garis, memberikan kesan klasik dan elegan.',NULL,NULL,1),
(22,'Dry Pique Tipping Polo Shirt',299000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/469052/item/goods_30_469052_3x4.jpg?width=369',-1,6,NULL,'Polo shirt bergaris dengan aksen tipping, menawarkan gaya preppy yang modern.',NULL,NULL,1),
(23,'Washable Milano Ribbed Crew Neck Long Sleeve Sweater',599000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/453754/sub/goods_453754_sub13_3x4.jpg?width=369',-1,7,NULL,'Sweater crew neck berbahan Milano ribbed yang lembut dan nyaman untuk cuaca dingin.',NULL,NULL,1),
(24,'Extra Fine Merino Crew Neck Long Sleeve Sweater',599000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/450535/sub/goods_450535_sub14_3x4.jpg?width=369',-1,7,NULL,'Sweater crew neck dengan bahan Extra Fine Merino, menawarkan kehangatan dan gaya premium.',NULL,NULL,1),
(25,'3D Knit Crew Neck Sweater',599000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/475296/item/idgoods_05_475296_3x4.jpg?width=369',-1,7,NULL,'Sweater 3D knit dengan detail desain yang rapi dan nyaman dipakai sehari-hari.',NULL,NULL,1),
(26,'KAWS + Warhol 2WAY Tote Bag',299000,150000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/473556/item/goods_01_473556_3x4.jpg?width=369',0,11,NULL,'Tote bag kolaborasi KAWS + Warhol, menampilkan desain artistik yang eksklusif.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(27,'Crossbody Bag',299000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/476641/item/goods_02_476641_3x4.jpg?width=369',38,11,NULL,'Tas selempang kecil yang praktis untuk membawa kebutuhan harian.',NULL,NULL,1),
(28,'Round Mini Shoulder Bag (Water-Repellent)',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/461053/item/goods_30_461053_3x4.jpg?width=369',50,11,NULL,'Tas selempang mini dengan bahan tahan air, cocok untuk gaya aktif.',NULL,NULL,1),
(30,'HEATTECH Souffle Yarn Beanie',149000,50000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/471144/item/goods_08_471144_3x4.jpg?width=369',50,11,NULL,'Beanie berbahan HEATTECH yang memberikan kehangatan optimal selama musim dingin.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(32,'Italian Leather Garrison Belt',399000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/476853/item/goods_09_476853_3x4.jpg?width=369',50,11,NULL,'Ikat pinggang kulit Italia dengan desain elegan, cocok untuk acara formal.',NULL,NULL,1),
(33,'Leather Wide Mesh Belt',399000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/442224/item/goods_36_442224_3x4.jpg?width=369',62,11,NULL,'Ikat pinggang kulit dengan desain mesh yang memberikan tampilan modern.',NULL,NULL,1),
(34,'Sunglasses | Wellington Folding',299000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/475132/item/goods_09_475132_3x4.jpg?width=369',62,11,NULL,'Kacamata lipat Wellington dengan desain klasik dan portabilitas tinggi.',NULL,NULL,1),
(35,'HEATTECH Scarf',299000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/470021/item/goods_01_470021_3x4.jpg?width=369',77,11,NULL,'Syal berbahan HEATTECH, sempurna untuk melindungi leher dari cuaca dingin.',NULL,NULL,1),
(36,'AIRism Seamless Boxer Briefs | Print',149000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/477651/item/goods_08_477651_3x4.jpg?width=369',-1,9,NULL,'Celana boxer seamless dengan motif print, memberikan kenyamanan maksimal.',NULL,NULL,1),
(37,'AIRism V Neck Short Sleeve T-Shirt',149000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/454311/item/idgoods_00_454311_3x4.jpg?width=369',-1,9,NULL,'T-shirt AIRism dengan potongan V-neck, memberikan kenyamanan bernapas untuk aktivitas sehari-hari.',NULL,NULL,1),
(38,'AIRism Cotton T Dress | Short Sleeve',399000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/474062/item/idgoods_30_474062_3x4.jpg?width=369',-1,14,NULL,'Dress berbahan katun AIRism dengan desain santai dan elegan.',NULL,NULL,1),
(39,'Combination Dress Sleeveless',499000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/474064/sub/goods_474064_sub14_3x4.jpg?width=369',-1,14,NULL,'Dress sleeveless dengan kombinasi bahan yang memberikan gaya chic.',NULL,NULL,1),
(40,'Linen Blend Relax Dress | Short Sleeve',687000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/477618/item/idgoods_06_477618_3x4.jpg?width=369',-1,14,NULL,'Dress linen blend yang longgar dan nyaman, sempurna untuk cuaca hangat.',NULL,NULL,1),
(41,'Ribbed Bra Dress | Sleeveless',499000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/474080/sub/idgoods_474080_sub7_3x4.jpg?width=369',-1,14,NULL,'Dress sleeveless dengan bahan ribbed yang nyaman dan fleksibel.',NULL,NULL,1),
(42,'Denim Shirt Dress | Long Sleeve',699000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/476150/sub/goods_476150_sub14_3x4.jpg?width=369',-1,14,NULL,'Dress berbahan denim dengan lengan panjang, memberikan gaya casual yang santai.',NULL,NULL,1),
(43,'Chiffon Skirt Print',599000,100000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/475172/item/goods_69_475172_3x4.jpg?width=369',-1,14,NULL,'Rok chiffon dengan motif print yang anggun dan feminin.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(44,'Ultra Stretch Narrow Skirt | Denim',399000,200000,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/476129/sub/idgoods_476129_sub23_3x4.jpg?width=369',-1,14,NULL,'Rok denim dengan desain ultra stretch, memberikan kenyamanan dan fleksibilitas.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(45,'Pleated Long Skirt',699000,300000,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/475399/item/idgoods_08_475399_3x4.jpg?width=369',-1,14,NULL,'Rok panjang dengan detail lipit, memberikan kesan anggun dan modern.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(46,'Wireless Bra | 3D Hold',299000,100000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/473913/sub/goods_473913_sub13_3x4.jpg?width=369',-1,19,NULL,'Bra nirkawat dengan desain 3D hold, nyaman dan mendukung bentuk tubuh.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(47,'AIRism Seamless Shorts | Just Waist',99000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/473945/item/goods_10_473945_3x4.jpg?width=369',-1,19,NULL,'Celana pendek seamless dengan pinggang elastis yang nyaman.',NULL,NULL,1),
(48,'Hiphugger Briefs | Lace',79000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/472615/item/goods_12_472615_3x4.jpg?width=369',-1,19,NULL,'Celana hiphugger dengan renda yang elegan dan feminin.',NULL,NULL,1),
(49,'HEATTECH Ultra Warm Leggings',399000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/469844/item/idgoods_03_469844_3x4.jpg?width=369',-1,19,NULL,'Legging ultra hangat berbahan HEATTECH, ideal untuk musim dingin.',NULL,NULL,1),
(50,'Ultra Stretch Maternity Leggings Pants | Denim',599000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/464153/item/idgoods_68_464153_3x4.jpg?width=369',-1,19,NULL,'Legging maternity ultra stretch berbahan denim, mendukung kenyamanan selama kehamilan.',NULL,NULL,1),
(51,'AIRism Cotton Relaxed Long T-Shirt | Long Sleeve',249000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/477686/item/idgoods_52_477686_3x4.jpg?width=369',-1,21,NULL,'T-shirt katun longgar dengan lengan panjang, cocok untuk gaya santai.',NULL,NULL,1),
(52,'Rayon Gathered Tunic | Long Sleeve',399000,100000,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/473356/item/idgoods_18_473356_3x4.jpg?width=369',-1,21,NULL,'Tunik berbahan rayon dengan detail gathered, memberikan tampilan yang anggun.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(53,'KIDS AIRism Cotton Crew Neck T-Shirt',149000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/465136/item/idgoods_30_465136_3x4.jpg?width=369',-1,27,NULL,'- Smooth \'AIRism\' fabric with the look of cotton.\r\n- Back collar seam covered with tape for comfort.\r\n- Relaxed unisex fit with a sleek cut at the shoulders to create a soft impression.\r\n- Roomy cut around the waist.\r\n- Design features side slits.\r\n- Ribbed neckline.\r\n- Dropped shoulders.\r\n- Versatile style can be worn all year round.',NULL,NULL,1),
(54,'GIRLS Soft Ribbed Mini T-Shirt | Ringer',149000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/474773/item/idgoods_60_474773_3x4.jpg?width=369',-1,27,NULL,'- Cropped, compact silhouette works well with casual and sporty outfits.',NULL,NULL,1),
(55,'KIDS AIRism Cotton Graphic Crew Neck T-Shirt | Ringer',149000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/474601/sub/goods_474601_sub14_3x4.jpg?width=369',-1,27,NULL,'- Relaxed silhouette is perfect for unisex wear.\r\n- A versatile wardrobe essential.',NULL,NULL,1),
(56,'KIDS Wide Fit Straight Jeans | Zip Fly',299000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/477652/item/idgoods_01_477652_3x4.jpg?width=369',-1,26,NULL,'- Soft and comfortable 100% cotton. Authentic texture and genuine color.\r\n- To adjust the fit, pull the elastic waistband through the opening and button at the desired location.\r\n- Waist button with a hook design for easy dressing.',NULL,NULL,1),
(57,'KIDS Fleece Full-Zip Jacket | Long Sleeve',299000,100000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/472079/sub/goods_472079_sub13_3x4.jpg?width=369',-1,25,NULL,'- Soft, warm, and lightweight fleece.\r\n- High collar and piping at the hem and sleeves help keep kids warm.\r\n- Machine-washable for easy care.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(58,'KIDS Dry Sweat Full-Zip Jacket | Line',299000,0,'https://image.uniqlo.com/UQ/ST3/id/imagesgoods/474835/item/idgoods_66_474835_3x4.jpg?width=369',-1,25,NULL,'- With DRY technology.\r\n- Ultra Stretch fabric for comfort and easy movement.\r\n- Raglan sleeves provide freedom of movement.\r\n- Track jacket design with sporty stripes.\r\n- The back collar seam is covered with tape for comfort.',NULL,NULL,1),
(59,'Ribbed One Piece Outfit Stripe | Long Sleeve',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/469568/item/goods_68_469568_3x4.jpg?width=369',-1,28,NULL,'- Versatile striped pattern.\r\nThis product cannot be returned.',NULL,NULL,1),
(60,'One Piece Outfit | Long Sleeve',199000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/469567/item/goods_41_469567_3x4.jpg?width=369',-1,28,NULL,'Joy of Print is a London based independent print studio founded by the designer Cath Kidston as a new solo project, introducing a collection flowing with beauty and joy.\r\n\r\n- Animal applique at the chest.\r\nThis product cannot be returned.',NULL,NULL,1),
(61,'Lisa Larson Bodysuit | Long Sleeve',99000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/473836/item/goods_02_473836_3x4.jpg?width=369',-1,28,NULL,'Lisa Larson\r\n\r\nLisa Larson is loved around the world for her many works with unique perspectives and adorable forms, such as her cat and lion animal series. This special design is featuring with animals and gorgeous motifs, including “MIKEY,” the cat created by Lisa.\r\n\r\nLISA LARSON\r\nCeramicist / Born in 1931 in Sweden. In 1954, Lisa began working as a designer for Gustavsberg porcelain. She became a widely recognized artist in the golden age of Swedish ceramics. In 1992, she co-founded Keramikstudion Gustavsberg. She continued creating new designs for production, and unique ceramic sculptures until her death in 2024. Although she is no longer with us, her creative spirit lives on.\r\n\r\n© LISA LARSON\r\nThis product cannot be returned.',NULL,NULL,1),
(62,'Dry Crew Neck T-Shirt | Short Sleeve',149000,50000,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/464604/item/goods_42_464604_3x4.jpg?width=369',-1,29,NULL,'- The patch pocket on the chest is embroidered with palm tree and fruit motifs.\r\n- Relaxed boxy cut.\r\n- Stretchy fabric for easy movement, with quick-drying DRY technology.\r\n- Hypoallergenic plastic snap-fasteners. *Note: May not protect against all allergies.\r\n- 70-90 cm sizes have snap button openings at the shoulders.\r\n- 100-110 cm sizes do not have snap buttons at the shoulders.\r\nThis product cannot be returned.','2025-01-07 09:34:46','2025-01-20 09:34:50',1),
(63,'Dry Crew Neck T-Shirt Joy of Print | Short Sleeve',149000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/473626/item/goods_01_473626_3x4.jpg?width=369',-1,29,NULL,'Joy of Print is a London based independent print studio founded by the designer Cath Kidston as a new solo project, introducing a collection flowing with beauty and joy.\r\n\r\n- Relaxed silhouette.\r\nThis product cannot be returned.',NULL,NULL,1),
(64,'Lisa Larson Pajamas Long Sleeve',299000,0,'https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/473886/item/goods_13_473886_3x4.jpg?width=369',-1,29,NULL,'Lisa Larson\r\n\r\nLisa Larson is loved around the world for her many works with unique perspectives and adorable forms, such as her cat and lion animal series. This special design is featuring with animals and gorgeous motifs, including “MIKEY,” the cat created by Lisa.\r\n\r\nLISA LARSON\r\nCeramicist / Born in 1931 in Sweden. In 1954, Lisa began working as a designer for Gustavsberg porcelain. She became a widely recognized artist in the golden age of Swedish ceramics. In 1992, she co-founded Keramikstudion Gustavsberg. She continued creating new designs for production, and unique ceramic sculptures until her death in 2024. Although she is no longer with us, her creative spirit lives on.\r\n\r\n© LISA LARSON\r\nThis product cannot be returned.',NULL,NULL,1);

/*Table structure for table `cart` */

DROP TABLE IF EXISTS `cart`;

CREATE TABLE `cart` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_user` int(11) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `status` enum('pending','process') DEFAULT 'pending',
  PRIMARY KEY (`id`),
  KEY `foreign_cart_user` (`id_user`),
  CONSTRAINT `foreign_cart_user` FOREIGN KEY (`id_user`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `cart` */

insert  into `cart`(`id`,`id_user`,`created_at`,`status`) values 
(10,7,'2025-01-08 11:39:50','pending');

/*Table structure for table `d_cart` */

DROP TABLE IF EXISTS `d_cart`;

CREATE TABLE `d_cart` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_cart` int(11) DEFAULT NULL,
  `id_barang` int(11) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `harga` int(11) DEFAULT NULL,
  `diskon` int(11) DEFAULT NULL,
  `subtotal` int(11) DEFAULT NULL,
  `size` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `foreign_dcart_card` (`id_cart`),
  KEY `foreign_dcart_barang` (`id_barang`),
  CONSTRAINT `foreign_dcart_barang` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`),
  CONSTRAINT `foreign_dcart_cart` FOREIGN KEY (`id_cart`) REFERENCES `cart` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `d_cart` */

/*Table structure for table `d_trans` */

DROP TABLE IF EXISTS `d_trans`;

CREATE TABLE `d_trans` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_htrans` int(11) DEFAULT NULL,
  `id_barang` int(11) DEFAULT NULL,
  `nama_barang` varchar(255) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `harga` int(11) DEFAULT NULL,
  `diskon` int(11) DEFAULT NULL,
  `subtotal` int(11) DEFAULT NULL,
  `size` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `foreign_dtrans_barang` (`id_barang`),
  KEY `foreign_dtrans_htrans` (`id_htrans`),
  CONSTRAINT `foreign_dtrans_barang` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`),
  CONSTRAINT `foreign_dtrans_htrans` FOREIGN KEY (`id_htrans`) REFERENCES `h_trans` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `d_trans` */

insert  into `d_trans`(`id`,`id_htrans`,`id_barang`,`nama_barang`,`quantity`,`harga`,`diskon`,`subtotal`,`size`) values 
(1,1,5,'dvefvger',1213,2432423,213423,213424,'XL'),
(10,6,5,'DRY-EX T-Shirt',3,249000,0,747000,'S');

/*Table structure for table `h_trans` */

DROP TABLE IF EXISTS `h_trans`;

CREATE TABLE `h_trans` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_user` int(11) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `foreign_htrans_user` (`id_user`),
  CONSTRAINT `foreign_htrans_user` FOREIGN KEY (`id_user`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `h_trans` */

insert  into `h_trans`(`id`,`id_user`,`created_at`) values 
(1,1,'2025-01-07 13:19:28'),
(6,7,'2025-01-08 10:05:01');

/*Table structure for table `kategori` */

DROP TABLE IF EXISTS `kategori`;

CREATE TABLE `kategori` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nama` varchar(255) DEFAULT NULL,
  `id_pengguna` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_jenis_kategori` (`id_pengguna`),
  CONSTRAINT `foreign_kategori_pengguna` FOREIGN KEY (`id_pengguna`) REFERENCES `pengguna` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `kategori` */

insert  into `kategori`(`id`,`nama`,`id_pengguna`) values 
(1,'T-Shirt',2),
(2,'Bawahan',2),
(3,'T-Shirt',1),
(4,'Celana',1),
(5,'Outerwear',2),
(6,'Polo Shirts',2),
(7,'Knitwear',2),
(8,'Bottoms',2),
(9,'Innerwear',2),
(10,'Loungewear',2),
(11,'Accessories',2),
(12,'SPORT UTILITY WEAR',2),
(13,'UT Graphic T-Shirts',2),
(14,'Skirts & Dresses',1),
(15,'Knitwear',1),
(16,'Outerwear',1),
(17,'Bottoms',1),
(18,'Loungewear',1),
(19,'Innerwear',1),
(20,'Accessories',1),
(21,'Modest Wear',1),
(22,'SPORT UTILITY WEAR',1),
(23,'UT Graphic T-Shirts',1),
(24,'Blouses',1),
(25,'Outerwear',3),
(26,'Bottoms',3),
(27,'T-Shirt',3),
(28,'Newborn (0 - 1 Years)',4),
(29,'Toddler (1 - 4 Years)',4);

/*Table structure for table `pengguna` */

DROP TABLE IF EXISTS `pengguna`;

CREATE TABLE `pengguna` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nama` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `pengguna` */

insert  into `pengguna`(`id`,`nama`) values 
(1,'Wanita'),
(2,'Pria'),
(3,'Anak'),
(4,'Bayi');

/*Table structure for table `stok` */

DROP TABLE IF EXISTS `stok`;

CREATE TABLE `stok` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_barang` int(11) DEFAULT NULL,
  `size` varchar(5) DEFAULT NULL,
  `stok` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `foreign_stok_barang` (`id_barang`),
  CONSTRAINT `foreign_stok_barang` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=309 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `stok` */

insert  into `stok`(`id`,`id_barang`,`size`,`stok`) values 
(1,3,'XS',4),
(2,3,'S',0),
(3,3,'M',6),
(4,3,'L',1),
(5,3,'XL',2),
(6,3,'XXL',0),
(7,3,'3XL',0),
(8,4,'XS',0),
(9,4,'S',0),
(10,4,'M',0),
(11,4,'L',0),
(12,4,'XL',0),
(13,4,'XXL',0),
(14,4,'3XL',0),
(15,5,'XS',0),
(16,5,'S',45),
(17,5,'M',50),
(18,5,'L',50),
(19,5,'XL',50),
(20,5,'XXL',50),
(21,5,'3XL',50),
(22,6,'XS',50),
(23,6,'S',50),
(24,6,'M',50),
(25,6,'L',50),
(26,6,'XL',50),
(27,6,'XXL',50),
(28,6,'3XL',50),
(29,7,'XS',50),
(30,7,'S',50),
(31,7,'M',50),
(32,7,'L',50),
(33,7,'XL',50),
(34,7,'XXL',50),
(35,7,'3XL',50),
(36,8,'XS',50),
(37,8,'S',49),
(38,8,'M',50),
(39,8,'L',50),
(40,8,'XL',50),
(41,8,'XXL',50),
(42,8,'3XL',50),
(43,9,'XS',50),
(44,9,'S',50),
(45,9,'M',50),
(46,9,'L',50),
(47,9,'XL',50),
(48,9,'XXL',50),
(49,9,'3XL',50),
(50,10,'XS',50),
(51,10,'S',50),
(52,10,'M',50),
(53,10,'L',50),
(54,10,'XL',50),
(55,10,'XXL',50),
(56,10,'3XL',50),
(57,11,'XS',50),
(58,11,'S',50),
(59,11,'M',50),
(60,11,'L',50),
(61,11,'XL',50),
(62,11,'XXL',50),
(63,11,'3XL',50),
(64,12,'XS',50),
(65,12,'S',50),
(66,12,'M',50),
(67,12,'L',50),
(68,12,'XL',50),
(69,12,'XXL',50),
(70,12,'3XL',50),
(71,13,'XS',45),
(72,13,'S',50),
(73,13,'M',50),
(74,13,'L',50),
(75,13,'XL',50),
(76,13,'XXL',50),
(77,13,'3XL',50),
(78,14,'XS',50),
(79,14,'S',50),
(80,14,'M',50),
(81,14,'L',50),
(82,14,'XL',50),
(83,14,'XXL',50),
(84,14,'3XL',50),
(85,15,'XS',50),
(86,15,'S',50),
(87,15,'M',50),
(88,15,'L',50),
(89,15,'XL',50),
(90,15,'XXL',50),
(91,15,'3XL',50),
(92,16,'XS',50),
(93,16,'S',50),
(94,16,'M',50),
(95,16,'L',50),
(96,16,'XL',50),
(97,16,'XXL',50),
(98,16,'3XL',50),
(99,17,'XS',50),
(100,17,'S',50),
(101,17,'M',50),
(102,17,'L',50),
(103,17,'XL',50),
(104,17,'XXL',50),
(105,17,'3XL',50),
(106,18,'XS',50),
(107,18,'S',50),
(108,18,'M',50),
(109,18,'L',50),
(110,18,'XL',50),
(111,18,'XXL',50),
(112,18,'3XL',50),
(113,19,'XS',50),
(114,19,'S',50),
(115,19,'M',50),
(116,19,'L',59),
(117,19,'XL',50),
(118,19,'XXL',50),
(119,19,'3XL',50),
(120,20,'XS',50),
(121,20,'S',50),
(122,20,'M',50),
(123,20,'L',59),
(124,20,'XL',50),
(125,20,'XXL',50),
(126,20,'3XL',50),
(127,21,'XS',50),
(128,21,'S',50),
(129,21,'M',50),
(130,21,'L',59),
(131,21,'XL',50),
(132,21,'XXL',50),
(133,21,'3XL',50),
(134,22,'XS',50),
(135,22,'S',50),
(136,22,'M',50),
(137,22,'L',59),
(138,22,'XL',50),
(139,22,'XXL',50),
(140,22,'3XL',50),
(141,23,'XS',50),
(142,23,'S',50),
(143,23,'M',50),
(144,23,'L',71),
(145,23,'XL',50),
(146,23,'XXL',50),
(147,23,'3XL',50),
(148,24,'XS',50),
(149,24,'S',50),
(150,24,'M',50),
(151,24,'L',50),
(152,24,'XL',50),
(153,24,'XXL',50),
(154,24,'3XL',50),
(155,25,'XS',50),
(156,25,'S',50),
(157,25,'M',50),
(158,25,'L',50),
(159,25,'XL',50),
(160,25,'XXL',50),
(161,25,'3XL',50),
(162,36,'XS',50),
(163,36,'S',50),
(164,36,'M',50),
(165,36,'L',50),
(166,36,'XL',50),
(167,36,'XXL',50),
(168,36,'3XL',50),
(169,37,'XS',50),
(170,37,'S',50),
(171,37,'M',50),
(172,37,'L',50),
(173,37,'XL',50),
(174,37,'XXL',50),
(175,37,'3XL',50),
(176,38,'XS',50),
(177,38,'S',50),
(178,38,'M',50),
(179,38,'L',50),
(180,38,'XL',50),
(181,38,'XXL',50),
(182,38,'3XL',50),
(183,39,'XS',50),
(184,39,'S',50),
(185,39,'M',50),
(186,39,'L',50),
(187,39,'XL',50),
(188,39,'XXL',50),
(189,39,'3XL',50),
(190,40,'XS',50),
(191,40,'S',50),
(192,40,'M',50),
(193,40,'L',50),
(194,40,'XL',50),
(195,40,'XXL',50),
(196,40,'3XL',50),
(197,41,'XS',50),
(198,41,'S',50),
(199,41,'M',95),
(200,41,'L',50),
(201,41,'XL',50),
(202,41,'XXL',50),
(203,41,'3XL',50),
(204,42,'XS',50),
(205,42,'S',50),
(206,42,'M',95),
(207,42,'L',50),
(208,42,'XL',50),
(209,42,'XXL',50),
(210,42,'3XL',50),
(211,43,'XS',50),
(212,43,'S',50),
(213,43,'M',50),
(214,43,'L',50),
(215,43,'XL',50),
(216,43,'XXL',50),
(217,43,'3XL',50),
(218,44,'XS',50),
(219,44,'S',50),
(220,44,'M',50),
(221,44,'L',50),
(222,44,'XL',50),
(223,44,'XXL',50),
(224,45,'S',50),
(225,45,'M',50),
(226,45,'L',50),
(227,46,'XS',50),
(228,46,'S',50),
(229,46,'M',50),
(230,46,'L',50),
(231,46,'XL',50),
(232,46,'XXL',50),
(233,46,'3XL',50),
(234,47,'XS',50),
(235,47,'S',50),
(236,47,'M',50),
(237,47,'L',50),
(238,47,'XL',50),
(239,47,'XXL',50),
(240,48,'XS',50),
(241,48,'S',50),
(242,48,'M',50),
(243,48,'L',50),
(244,48,'XL',50),
(245,48,'XXL',50),
(246,49,'XS',50),
(247,49,'S',50),
(248,49,'M',50),
(249,49,'L',50),
(250,49,'XL',50),
(251,49,'XXL',50),
(252,49,'3XL',50),
(253,50,'L',50),
(254,50,'XL',50),
(255,51,'XS',50),
(256,51,'S',50),
(257,51,'M',50),
(258,51,'L',50),
(259,51,'XL',50),
(260,51,'XXL',50),
(261,51,'3XL',50),
(262,52,'XS',50),
(263,52,'S',50),
(264,52,'M',50),
(265,52,'L',50),
(266,52,'XL',50),
(267,52,'XXL',50),
(268,53,'XS',50),
(269,53,'S',50),
(270,53,'M',50),
(271,53,'L',47),
(272,54,'XS',50),
(273,54,'S',50),
(274,54,'M',50),
(275,54,'L',50),
(276,55,'XS',50),
(277,55,'S',50),
(278,55,'M',50),
(279,55,'L',50),
(280,56,'XS',50),
(281,56,'S',50),
(282,56,'M',50),
(283,56,'L',50),
(284,57,'XS',50),
(285,57,'S',50),
(286,57,'M',50),
(287,57,'L',50),
(288,57,'XL',50),
(289,58,'XS',50),
(290,58,'S',50),
(291,58,'M',50),
(292,58,'L',50),
(293,58,'XL',50),
(294,59,'XS',50),
(295,59,'S',50),
(296,60,'XS',50),
(297,60,'S',50),
(298,61,'XS',50),
(299,61,'S',50),
(300,62,'XS',50),
(301,62,'S',50),
(302,62,'M',50),
(303,63,'XS',50),
(304,63,'S',50),
(305,63,'M',50),
(306,64,'XS',50),
(307,64,'S',50),
(308,64,'M',50);

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nama` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `role` enum('Admin','Customer','Cashier') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `user` */

insert  into `user`(`id`,`nama`,`username`,`password`,`role`) values 
(1,'Jason','123','123','Customer'),
(2,'Jason','321','321','Customer'),
(3,'prit','abc','abc','Admin'),
(4,'prit','cba','cba','Cashier'),
(5,'prit','idk','c6e5e46c59267114f91d64df0e069b0dae176f9a134656820bba1e6164318980','Admin'),
(6,'tr','111','f6e0a1e2ac41945a9aa7ff8a8aaa0cebc12a3bcc981a929ad5cf810a090e11ae','Cashier'),
(7,'vis','tr','7817bb812e82168bd48fe1ea6783078d42be37e8db9bdaafdac5c45804aca64f','Customer');

/* Trigger structure for table `d_cart` */

DELIMITER $$

/*!50003 DROP TRIGGER*//*!50032 IF EXISTS */ /*!50003 `after_delete_d_cart` */$$

/*!50003 CREATE */ /*!50017 DEFINER = 'root'@'localhost' */ /*!50003 TRIGGER `after_delete_d_cart` AFTER DELETE ON `d_cart` FOR EACH ROW BEGIN
		IF OLD.size = 'NO' THEN
		-- Kembalikan stok barang di tabel 'barang'
		UPDATE barang
		SET stok_nosize = stok_nosize + OLD.quantity
		WHERE id = OLD.id_barang;
	    ELSE
		-- Kembalikan stok barang di tabel 'stok' berdasarkan ukuran
		UPDATE stok
		SET stok = stok + OLD.quantity
		WHERE id_barang = OLD.id_barang AND size = OLD.size;
	    END IF;
    END */$$


DELIMITER ;

/*!50106 set global event_scheduler = 1*/;

/* Event structure for event `delete_cart` */

/*!50106 DROP EVENT IF EXISTS `delete_cart`*/;

DELIMITER $$

/*!50106 CREATE DEFINER=`root`@`localhost` EVENT `delete_cart` ON SCHEDULE EVERY 1 DAY STARTS '2025-01-09 00:00:00' ON COMPLETION NOT PRESERVE ENABLE DO BEGIN
	    DELETE FROM d_cart
	    WHERE cart_id IN (
		SELECT id FROM cart WHERE created_at < NOW() - INTERVAL 1 DAY
	    );

	    -- Hapus data di cart yang expired
	    DELETE FROM cart
	    WHERE created_at < NOW() - INTERVAL 1 DAY;
	END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

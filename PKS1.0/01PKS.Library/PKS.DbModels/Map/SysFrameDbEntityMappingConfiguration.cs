using PKS.DbModels;
using PKS.DbModels.Portal;
using PKS.DbModels.PortalMgmt;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKS.DBModels
{
    /// <summary>SysFrame数据访问实体映射配置</summary>
    public class SysFrameDbEntityMappingConfiguration : PKSDbEntityMappingConfiguration
    {
        /// <summary>Schema名称</summary>
        public override string SchemaName { get { return "SYSFRAME"; } }
        /// <summary>初始化实体映射</summary>
        protected override void OnModelCreating()
        {
            Entity<USERAUTHSESSIONS>();
            Entity<USERPROFILE>();
            Entity<WEBPAGES_MEMBERSHIP>();
            Entity<WEBPAGES_OAUTHMEMBERSHIP>();
            Entity<WEBPAGES_ROLES>();
            Entity<WEBPAGES_USERSINROLES>();
            Entity<VI_USERINFO>();

            //var mapping_PKS_Log = Entity<PKS_Log>();
            //mapping_PKS_Log.HasKey(p => p.Id)
            //    .Property(p => p.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //mapping_PKS_Log.Property(p => p.Request).IsUnicode(true);
            //mapping_PKS_Log.Property(p => p.Message).IsUnicode(true);
            //mapping_PKS_Log.Property(p => p.Source).IsUnicode(true);
            //mapping_PKS_Log.Property(p => p.Exception).IsUnicode(true).HasColumnType("NCLOB");

            var mapping_PKS_SUBSYSTEM = Entity<PKS_SUBSYSTEM>(true);
            mapping_PKS_SUBSYSTEM.HasKey(p => p.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapping_PKS_SUBSYSTEM.HasMany(p => p.Permissions)
                .WithRequired()
                .HasForeignKey(t => t.SubSystemId);

            var pt_map = Entity<PKS_PERMISSION_TYPE>();
            pt_map.HasMany(t => t.Permissions)
                  .WithRequired()
                  .HasForeignKey(p => p.PermissionTypeId);

            var p_map = Entity<PKS_PERMISSION>(true);
            p_map.HasKey(p => p.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            p_map.HasMany(p => p.RolePermissions)
                .WithRequired(r => r.Permission)
                .HasForeignKey(r => r.PermissionId);

            Entity<PKS_ROLE_PERMISSION>(true)
                .HasKey(p => p.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            var map_PKS_Remark = Entity<PKS_Remark>();
            map_PKS_Remark.HasMany(t => t.Thumbups).WithRequired().HasForeignKey(p => p.RemarkId);

            Entity<PKS_Remark_Thumbup>();

            Entity<PKS_SearchHistory>();
            
        }
    }
}

using System;
using Edelweiss.TextureAtlas;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	public abstract class GenericDecalsBase : MonoBehaviour
	{
		private Transform m_CachedTransform;

		public const int c_MaximumVertexCount = 65535;

		private GenericDecalsMeshBase m_LinkedDecalsMesh;

		[SerializeField]
		private ProjectionMode m_ProjectionMode;

		[SerializeField]
		private UVMode m_UVMode;

		[SerializeField]
		private UV2Mode m_UV2Mode;

		[SerializeField]
		private NormalsMode m_NormalsMode = NormalsMode.Target;

		[SerializeField]
		private TangentsMode m_TangentsMode;

		[SerializeField]
		private bool m_UseVertexColors;

		[SerializeField]
		private Color m_VertexColorTint = Color.white;

		[SerializeField]
		private bool m_AffectSameLODOnly;

		[SerializeField]
		private LightmapUpdateMode m_LightmapUpdateMode;

		[SerializeField]
		private bool m_AreRenderersEditable;

		[SerializeField]
		private TextureAtlasType m_TextureAtlasType;

		[SerializeField]
		private TextureAtlasAsset m_TextureAtlasAsset;

		[SerializeField]
		private Material m_Material;

		public UVRectangle[] uvRectangles = new UVRectangle[0];

		public UVRectangle[] uv2Rectangles = new UVRectangle[0];

		[SerializeField]
		private bool m_AreDecalsMeshesOptimized;

		[SerializeField]
		private string m_MeshAssetFolder = "Assets";

		public Transform CachedTransform
		{
			get
			{
				if (m_CachedTransform == null)
				{
					m_CachedTransform = base.transform;
				}
				return m_CachedTransform;
			}
		}

		public GenericDecalsMeshBase LinkedDecalsMesh
		{
			get
			{
				return m_LinkedDecalsMesh;
			}
			internal set
			{
				m_LinkedDecalsMesh = value;
			}
		}

		public bool IsLinkedWithADecalsMesh
		{
			get
			{
				return LinkedDecalsMesh != null;
			}
		}

		public abstract bool CastShadows { get; set; }

		public abstract bool ReceiveShadows { get; set; }

		public abstract bool UseLightProbes { get; set; }

		public abstract Transform LightProbeAnchor { get; set; }

		public ProjectionMode CurrentProjectionMode
		{
			get
			{
				return m_ProjectionMode;
			}
			set
			{
				if (IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The projection mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				m_ProjectionMode = value;
				if (m_ProjectionMode == ProjectionMode.Diffuse)
				{
					m_UVMode = UVMode.Project;
					m_UV2Mode = UV2Mode.None;
					m_NormalsMode = NormalsMode.Target;
					m_TangentsMode = TangentsMode.None;
				}
				else if (m_ProjectionMode == ProjectionMode.BumpedDiffuse)
				{
					m_UVMode = UVMode.Project;
					m_UV2Mode = UV2Mode.None;
					m_NormalsMode = NormalsMode.Target;
					m_TangentsMode = TangentsMode.Project;
				}
				else if (m_ProjectionMode == ProjectionMode.LightmappedDiffuse)
				{
					m_UVMode = UVMode.Project;
					m_UV2Mode = UV2Mode.Lightmapping;
					m_NormalsMode = NormalsMode.Target;
					m_TangentsMode = TangentsMode.None;
				}
				else if (m_ProjectionMode == ProjectionMode.LightmappedBumpedDiffuse)
				{
					m_UVMode = UVMode.Project;
					m_UV2Mode = UV2Mode.Lightmapping;
					m_NormalsMode = NormalsMode.Target;
					m_TangentsMode = TangentsMode.Project;
				}
				else if (m_ProjectionMode == ProjectionMode.BumpOfTarget)
				{
					m_UVMode = UVMode.Project;
					m_UV2Mode = UV2Mode.TargetUV;
					m_NormalsMode = NormalsMode.Target;
					m_TangentsMode = TangentsMode.Target;
				}
			}
		}

		public UVMode CurrentUVMode
		{
			get
			{
				return m_UVMode;
			}
			set
			{
				if (IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The uv mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (CurrentProjectionMode == ProjectionMode.Advanced)
				{
					m_UVMode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new uv mode is only possible in the advanced projection mode!");
			}
		}

		public UV2Mode CurrentUV2Mode
		{
			get
			{
				return m_UV2Mode;
			}
			set
			{
				if (IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The uv2 mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (CurrentProjectionMode == ProjectionMode.Advanced)
				{
					m_UV2Mode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new uv2 mode is only possible in the advanced projection mode!");
			}
		}

		public NormalsMode CurrentNormalsMode
		{
			get
			{
				return m_NormalsMode;
			}
			set
			{
				if (IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The normals mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (CurrentProjectionMode == ProjectionMode.Advanced)
				{
					m_NormalsMode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new normals mode is only possible in the advanced projection mode!");
			}
		}

		public TangentsMode CurrentTangentsMode
		{
			get
			{
				return m_TangentsMode;
			}
			set
			{
				if (IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The tangents mode can't be changed as long as the instance is linked with a decals mesh.");
				}
				if (CurrentProjectionMode == ProjectionMode.Advanced)
				{
					m_TangentsMode = value;
					return;
				}
				throw new InvalidOperationException("Setting a new tangents mode is only possible in the advanced projection mode!");
			}
		}

		public bool UseVertexColors
		{
			get
			{
				return m_UseVertexColors;
			}
			set
			{
				if (IsLinkedWithADecalsMesh)
				{
					throw new InvalidOperationException("The vertex color mode can't be changed as long as the instance is linked with a decals mesh.'");
				}
				if (value && Edition.IsDecalSystemFree)
				{
					throw new InvalidOperationException("Vertex colors can only be used in Decal System Pro.");
				}
				m_UseVertexColors = value;
			}
		}

		public Color VertexColorTint
		{
			get
			{
				return m_VertexColorTint;
			}
			set
			{
				m_VertexColorTint = value;
			}
		}

		public bool AffectSameLODOnly
		{
			get
			{
				return m_AffectSameLODOnly;
			}
			set
			{
				if (Application.isPlaying)
				{
					throw new InvalidOperationException("This operation can only be executed in the editor, meaning while the application is not playing.");
				}
				m_AffectSameLODOnly = value;
			}
		}

		public LightmapUpdateMode LightmapUpdateMode
		{
			get
			{
				return m_LightmapUpdateMode;
			}
			set
			{
				if (Application.isPlaying)
				{
					throw new InvalidOperationException("This operation can only be executed in the editor, meaning while the application is not playing.");
				}
				m_LightmapUpdateMode = value;
			}
		}

		public bool AreRenderersEditable
		{
			get
			{
				return m_AreRenderersEditable;
			}
			set
			{
				if (Edition.IsDecalSystemFree && value)
				{
					throw new InvalidOperationException("The renderer editability can only be used in Decal System Pro.");
				}
				m_AreRenderersEditable = value;
				ApplyRenderersEditability();
			}
		}

		public TextureAtlasType CurrentTextureAtlasType
		{
			get
			{
				return m_TextureAtlasType;
			}
			set
			{
				if (Edition.IsDecalSystemFree && value == TextureAtlasType.Reference)
				{
					throw new InvalidOperationException("Texture atlas assets can only be used in Decal System Pro.");
				}
				m_TextureAtlasType = value;
				ApplyMaterialToMeshRenderers();
			}
		}

		public TextureAtlasAsset CurrentTextureAtlasAsset
		{
			get
			{
				return m_TextureAtlasAsset;
			}
			set
			{
				m_TextureAtlasAsset = value;
				if (!Edition.IsDecalSystemFree && CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					ApplyMaterialToMeshRenderers();
				}
			}
		}

		public Material CurrentMaterial
		{
			get
			{
				return m_Material;
			}
			set
			{
				m_Material = value;
				if (CurrentTextureAtlasType == TextureAtlasType.Builtin)
				{
					ApplyMaterialToMeshRenderers();
				}
			}
		}

		public UVRectangle[] CurrentUvRectangles
		{
			get
			{
				UVRectangle[] result = null;
				if (CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					if (CurrentTextureAtlasAsset != null)
					{
						result = CurrentTextureAtlasAsset.uvRectangles;
					}
				}
				else if (CurrentTextureAtlasType == TextureAtlasType.Builtin)
				{
					result = uvRectangles;
				}
				return result;
			}
		}

		public UVRectangle[] CurrentUv2Rectangles
		{
			get
			{
				UVRectangle[] result = null;
				if (CurrentTextureAtlasType == TextureAtlasType.Reference)
				{
					if (CurrentTextureAtlasAsset != null)
					{
						result = CurrentTextureAtlasAsset.uv2Rectangles;
					}
				}
				else if (CurrentTextureAtlasType == TextureAtlasType.Builtin)
				{
					result = uv2Rectangles;
				}
				return result;
			}
		}

		public bool AreDecalsMeshesOptimized
		{
			get
			{
				return m_AreDecalsMeshesOptimized;
			}
		}

		public string MeshAssetFolder
		{
			get
			{
				return m_MeshAssetFolder;
			}
			set
			{
				m_MeshAssetFolder = value;
			}
		}

		public abstract void OnEnable();

		public abstract void InitializeDecalsMeshRenderers();

		public virtual void OptimizeDecalsMeshes()
		{
			m_AreDecalsMeshesOptimized = true;
		}

		public void SetDecalsMeshesAreNotOptimized()
		{
			m_AreDecalsMeshesOptimized = false;
		}

		public virtual void ApplyMaterialToMeshRenderers()
		{
			if (Edition.IsDecalSystemFree && CurrentTextureAtlasType == TextureAtlasType.Reference)
			{
				throw new InvalidOperationException("Texture atlas assets are only supported in Decal System Pro.");
			}
		}

		public virtual void ApplyRenderersEditability()
		{
			if (Edition.IsDecalSystemFree && m_AreRenderersEditable)
			{
				m_AreRenderersEditable = false;
			}
		}
	}
}

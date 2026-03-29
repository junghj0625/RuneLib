using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



namespace Rune.Pools
{
    public abstract class Pool<TSelf, TAsset> : MonoPlusSingleton<Pool<TSelf, TAsset>>, IPreloadable where TSelf : Pool<TSelf, TAsset> where TAsset : UnityEngine.Object
    {
        public override void OnEnable()
        {
            base.OnEnable();

            IPreloadable.AddPreloadable(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            IPreloadable.RemovePreloadable(this);
        }


        public static IEnumerator Load(string tag)
        {
            yield return Utils.Asset.LoadAssets<TAsset>(tag, (handle) =>
            {
                foreach (var asset in handle.Result)
                {
                    string name = $"{tag}:{asset.name}";

                    SingletonInstance._assetsByKey[name] = asset;


                    if (!SingletonInstance._namesByTag.TryGetValue(tag, out var names))
                    {
                        SingletonInstance._namesByTag[tag] = names = new HashSet<string>();
                    }

                    names.Add(name);


                    if (!SingletonInstance._handleByTag.ContainsKey(tag))
                    {
                        SingletonInstance._handleByTag[tag] = handle;
                    }


                    SingletonInstance.SendAssetsToRequesters();
                }
            });
        }

        public static TAsset Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (SingletonInstance._assetsByKey.TryGetValue(key, out TAsset loadedAsset))
            {
                return loadedAsset;
            }
            else
            {
                return null;
            }
        }

        public static bool TryGet(string key, out TAsset asset)
        {
            // Check key
            if (string.IsNullOrEmpty(key))
            {
                asset = null;

                return false;
            }


            // Get Loaded asset
            if (SingletonInstance._assetsByKey.TryGetValue(key, out TAsset loadedAsset))
            {
                asset = loadedAsset;

                return true;
            }


            // Asset not found
            asset = null;

            return false;
        }

        public static bool Has(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;

            return SingletonInstance._assetsByKey.ContainsKey(key);
        }

        public static void Release(string tag)
        {
            if (SingletonInstance._handleByTag.TryGetValue(tag, out var handle))
            {
                Addressables.Release(handle);

                SingletonInstance._handleByTag.Remove(tag);
            }

            if (SingletonInstance._namesByTag.TryGetValue(tag, out var names))
            {
                foreach (var name in names)
                {
                    SingletonInstance._assetsByKey.Remove(name);
                }

                SingletonInstance._namesByTag.Remove(tag);
            }
        }

        public static void Request(IRequester requester, string key, Action<TAsset> callback)
        {
            // Check validity of key
            if (string.IsNullOrEmpty(key)) return;


            // Give asset
            if (SingletonInstance._assetsByKey.TryGetValue(key, out var asset))
            {
                callback?.Invoke(asset);

                return;
            }


            // Add request pending set
            if (!_requestPending.TryGetValue(key, out var requestPendingDic))
            {
                _requestPending[key] = requestPendingDic = new Dictionary<IRequester, Action<TAsset>>();
            }


            // Remove old request
            if (requestPendingDic.ContainsKey(requester))
            {
                requestPendingDic.Remove(requester);
            }
            
            
            // Add request
            requestPendingDic.Add(requester, callback);
        }

        public IEnumerator Preload()
        {
            foreach (var scheduledAssetTag in ScheduledAssetTags)
            {
                yield return Load(scheduledAssetTag);
            }
        }



        public bool IsPreloaded { get; set; } = false;



        public List<string> ScheduledAssetTags = new();



        private void SendAssetsToRequesters()
        {
            foreach (var (key, requestPendingDic) in _requestPending.ToList())
            {
                // Get asset
                var asset = Get(key);

                if (asset == null) continue;


                // Send asset to requesters
                foreach (var (requester, callback) in requestPendingDic.ToList())
                {
                    callback?.Invoke(asset);

                    requestPendingDic.Remove(requester);
                }


                // Remove empty request pending dictionary
                if (requestPendingDic.Count == 0) _requestPending.Remove(key);
            }
        }



        protected Dictionary<string, TAsset> _assetsByKey = new();

        protected Dictionary<string, HashSet<string>> _namesByTag = new();

        protected Dictionary<string, AsyncOperationHandle<IList<TAsset>>> _handleByTag = new();



        private static readonly Dictionary<string, Dictionary<IRequester, Action<TAsset>>> _requestPending = new();
    }



    /* Example of Pool */

    public class PoolExample : Pool<PoolExample, TextAsset>
    {

    }
}
﻿/*******************************************************************************
 * Copyright 2012-2014 One Platform Foundation
 *
 *       Licensed under the Apache License, Version 2.0 (the "License");
 *       you may not use this file except in compliance with the License.
 *       You may obtain a copy of the License at
 *
 *           http://www.apache.org/licenses/LICENSE-2.0
 *
 *       Unless required by applicable law or agreed to in writing, software
 *       distributed under the License is distributed on an "AS IS" BASIS,
 *       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *       See the License for the specific language governing permissions and
 *       limitations under the License.
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace OnePF
{
    /** 
     * Main class 
     */
    public class OpenIAB
    {
        public static GameObject EventManager { get { return GameObject.Find(typeof(OpenIABEventManager).ToString()); } }

        static IOpenIAB _billing;

        static OpenIAB()
        {
#if UNITY_ANDROID
			_billing = new OpenIAB_Android();
            Debug.Log("********** Android OpenIAB plugin initialized **********");
#elif UNITY_IOS
			_billing = new OpenIAB_iOS();
            Debug.Log("********** iOS OpenIAB plugin initialized **********");
#elif UNITY_WP8
            _billing = new OpenIAB_WP8();
            Debug.Log("********** WP8 OpenIAB plugin initialized **********");
#else
			Debug.LogError("OpenIAB billing currently not supported on this platform. Sorry.");
#endif
        }

        /**
         * Must be only called before init
         */ 
        public static void mapSku(string sku, string storeName, string storeSku)
        {
            _billing.mapSku(sku, storeName, storeSku);
        }

        /**
         * Starts up the billing service. This will also check to see if in app billing is supported and fire the appropriate event
         */
        public static void init(Options options)
        {
            _billing.init(options);
        }

        /**
         * Unbinds and shuts down the billing service
         */ 
        public static void unbindService()
        {
            _billing.unbindService();
        }

        /**
         * Checks if subscriptions are supported. Currently used only on Android
         */
        public static bool areSubscriptionsSupported()
        {
            return _billing.areSubscriptionsSupported();
        }

        /**
         * Sends a request to get all completed purchases
         */ 
        public static void queryInventory()
        {
            _billing.queryInventory();
        }

        /**
         * Sends a request to get all completed purchases and specified skus information
         */ 
        public static void queryInventory(string[] skus)
        {
            _billing.queryInventory(skus);
        }

        /**
         * Purchases the product with the given sku and developerPayload
         */
        public static void purchaseProduct(string sku, string developerPayload = "")
        {
            _billing.purchaseProduct(sku, developerPayload);
        }

        /**
         * Purchases the subscription with the given sku and developerPayload
         */ 
        public static void purchaseSubscription(string sku, string developerPayload = "")
        {
            _billing.purchaseSubscription(sku, developerPayload);
        }

        /**
         * Sends out a request to consume the product
         */ 
        public static void consumeProduct(Purchase purchase)
        {
            _billing.consumeProduct(purchase);
        }

        /**
         * Restore purchased items. iOS AppStore requirement
         */ 
        public static void restoreTransactions()
        {
            _billing.restoreTransactions();
        }

        /**
         * Is verbose logging enabled
         */ 
		public static bool isDebugLog()
        {
            return _billing.isDebugLog();
        }

        /**
         * Get more debug information
         */ 
        public static void enableDebugLogging(bool enabled)
        {
            _billing.enableDebugLogging(enabled);
        }

        /**
         * Get more debug information
         */
        public static void enableDebugLogging(bool enabled, string tag)
        {
            _billing.enableDebugLogging(enabled, tag);
        }
    }
}

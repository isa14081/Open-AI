/*******************************************************************************
 * Copyright 2013 One Platform Foundation
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

package org.onepf.oms.appstore;

import java.util.List;

import org.onepf.oms.Appstore;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

/**
 * User: Boris Minaev
 * Date: 28.05.13
 * Time: 3:03
 */
public class OpenAppstoreBillingService implements AppstoreInAppBillingService {
    static final private String TAG = OpenAppstoreBillingService.class.getSimpleName();
    
    OpenIabHelperBillingService mIabHelperBillingService;
    String mPublicKey;
    IabHelper mIabHelper;

    public OpenAppstoreBillingService(String publicKey, Context context, Intent billingIntent, Appstore appstore) {
        Log.d(TAG, "OpenAppstoreBillingService() publicKey: " + publicKey+ " context: " + context+ " billingIntent: " + billingIntent+ " appstore: " + appstore);
        mPublicKey = publicKey;
        mIabHelper = new IabHelper(context, publicKey, appstore);
        mIabHelperBillingService = new OpenIabHelperBillingService(context);
        mIabHelperBillingService.setServiceIntent(billingIntent);
    }

    @Override
    public void startSetup(IabHelper.OnIabSetupFinishedListener listener) {
        Log.d(TAG, "will mIabHelper.startSetup");
        mIabHelper.startSetup(listener);
    }

    @Override
    public void launchPurchaseFlow(Activity act, String sku, String itemType, int requestCode, IabHelper.OnIabPurchaseFinishedListener listener, String extraData) {
        mIabHelper.launchPurchaseFlow(act, sku, itemType, requestCode, listener, extraData);
    }

    @Override
    public boolean handleActivityResult(int requestCode, int resultCode, Intent data) {
        return mIabHelper.handleActivityResult(requestCode, resultCode, data);
    }

    @Override
    public Inventory queryInventory(boolean querySkuDetails, List<String> moreItemSkus, List<String> moreSubsSkus) throws IabException {
        return mIabHelper.queryInventory(querySkuDetails, moreItemSkus, moreSubsSkus);
    }

    @Override
    public void consume(Purchase itemInfo) throws IabException {
        mIabHelper.consume(itemInfo);
    }

    @Override
    public void dispose() {
        mIabHelper.dispose();
    }
}

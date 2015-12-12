<div class="page-header">
    <h1>Your Info</h1>
</div>

<p>
	Api Key : <code> {{apikey}} </code>
</p>

<p>
	Mobile Key : <code> {{mobilekey}} </code>
</p>


<p> Integrate the code </p>
<div>

  <!-- Nav tabs -->
  <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#php" aria-controls="php" role="tab" data-toggle="tab">PHP</a></li>
    <li role="presentation"><a href="#python" aria-controls="python" role="tab" data-toggle="tab">Python</a></li>
    <li role="presentation"><a href="#ruby" aria-controls="ruby" role="tab" data-toggle="tab">Ruby</a></li>
    <li role="presentation"><a href="#rest" aria-controls="settings" role="tab" data-toggle="tab">REST</a></li>
  </ul>

  <!-- Tab panes -->
  <div class="tab-content">
    <div role="tabpanel" class="tab-pane active" id="php">
    	<code>
    		<br/>
    		<?php
    			echo "\<\?php";
    		?>
    		<br/>
    		address['unique'] = $p->id;
    		<br/>
            $address['lat'] = $p->latitude;
            <br/>
            $address['lng'] = $p->longitude;
            <br/>
            $address['needPhoto'] = '1';
            <br/>
            $address['declinedPhotoTrialCount'] = '0';
            <br/>
            $address['price'] = $p->rent_monthly*100;
            <br/>
            $address['ownerPhone'] = $p->Users->phone_number;
            <br/>
            $address['title'] = $p->title;
            <br/>
            $address['address'] = $p->address;
            <br/>
            <br/>
            $ch = curl_init();
            <br/>
            <br/>
            curl_setopt($ch, CURLOPT_URL,"http://pieorpi.com/api/newAddress");
            <br/>
            curl_setopt($ch, CURLOPT_POST, 1);
            <br/>
            curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            <br/>
            'mobilekey: hurriyetemlakhackathon'
            <br/>
            ));
            <br/>
             curl_setopt($ch, CURLOPT_POSTFIELDS, 
            <br/>
                      http_build_query(array('address' => json_encode($address), 'apikey' => "hurriyetemlakhackathon")));
            <br/>

            <br/>
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            <br/>

            $server_output = curl_exec ($ch);
            <br/>

            curl_close ($ch);
            <br/>
            <?php
    			echo "?>";
    		?>
    	</code>
    </div>
    <div role="tabpanel" class="tab-pane" id="python">
    	<code>
    		python code here
    	</code>
    </div>
    <div role="tabpanel" class="tab-pane" id="ruby">
    	<code>
    		ruby code here
    	</code>
    </div>
    <div role="tabpanel" class="tab-pane" id="rest">
    	<code>
    		rest code here
    	</code>
    </div>
  </div>

</div>


<hr/>

<p>
	Photos
</p>










<hr/>

<a href="/provider/logout" class="btn btn-default btn-lg btn-block" >logout</a>



